using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using Yqun.BO.BusinessManager;
using BizCommon;
using Yqun.Services;
using RabbitMQ.Client.Events;
using FarPoint.Win.Spread;

namespace TestDataUploadWS
{
    /// <summary>
    /// 采集数据上传
    /// </summary>
    public class UploadCaiJiNew
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UploadCaiJiNew()
        {
        }

        public void StartApplyQueue(string qName)
        {
            ThreadParameter p = new ThreadParameter();
            p.QName = qName;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ApplyQueue), p);
        }

        private void ApplyQueue(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    QueueingBasicConsumer consumer = null;

                    var queue_name = channel.QueueDeclare(p.QName, true, false, false, null);//持久化队列

                    consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(p.QName, false, consumer);

                    Boolean hasNext = true;
                    while (hasNext)
                    {
                        byte[] body = null;
                        var message = "";
                        BasicDeliverEventArgs ea = null;
                        try
                        {
                            ea = consumer.Queue.Dequeue();
                            if (ea != null)
                            {
                                body = ea.Body;
                                message = Encoding.UTF8.GetString(body);
                                logger.Error(message);
                                string[] ss = message.Split(',');
                                if (ss.Length == 3)
                                {
                                    DealCaiJi(ss[0], new Guid(ss[1]), new Guid(ss[2]));
                                }
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(string.Format("【{0}】RabbitMQ ApplyQueue message:{1} error:{2}", p.QName, message, ex.ToString()));
                        }
                    }
                }
            }
        }

        private void DealCaiJi(String DBName, Guid DataID, Guid moduleID)
        {
            String docString = DBHelper.CallLocalService("Yqun.BO.BusinessManager.dll", "GetDocumentByID", new object[] { DataID }, DBName).ToString();
            JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(docString);
            if (doc != null)
            {
                
                FpSpread sp = ModuleCatch.GetModuleByID(moduleID, doc, DBName);
                if (sp == null)
                {
                    return;
                }
                List<JZFormulaData> CrossSheetLineFormulaInfos = DBHelper.CallLocalService("Yqun.BO.BusinessManager.dll", "GetLineFormulaByModuleIndex", new object[] { moduleID }, DBName) as List<JZFormulaData>;
                ProcessDoc(doc, sp, CrossSheetLineFormulaInfos);
                DBHelper.CallLocalService("Yqun.BO.BusinessManager.dll", "SyncSaveDoc", new object[] { doc, moduleID }, DBName);
            }
            else
            {
                logger.Info("【" + DBName + "】,[" + DataID + "] DealCaiJi is empty.");
            }
        }

        private SheetView GetSheetByID(FpSpread sp, Guid sheetID)
        {
            if (sp != null && sp.Sheets.Count > 0)
            {
                foreach (SheetView viem in sp.Sheets)
                {
                    if (sheetID == new Guid(viem.Tag.ToString()))
                    {
                        return viem;
                    }
                }
            }
            return null;
        }

        private void ProcessDoc(JZDocument doc, FpSpread sp, List<JZFormulaData> CrossSheetLineFormulaInfos)
        {
            foreach (SheetView sheet in sp.Sheets)
            {
                for (int i = 0; i < sheet.RowCount; i++)
                {
                    for (int j = 0; j < sheet.ColumnCount; j++)
                    {
                        Cell cell = sheet.Cells[i, j];
                        cell.Formula = "";
                        cell.Value = JZCommonHelper.GetCellValue(doc, new Guid(sheet.Tag.ToString()), cell.Column.Label + cell.Row.Label);
                    }
                }
            }
            sp.LoadFormulas(true);
            foreach (JZFormulaData formula in CrossSheetLineFormulaInfos)
            {
                SheetView Sheet = GetSheetByID(sp, formula.SheetIndex);
                if (Sheet != null)
                {
                    Cell cell = Sheet.Cells[formula.RowIndex, formula.ColumnIndex];
                    if (cell != null)
                    {
                        try
                        {
                            if (formula.Formula.ToUpper().Trim() == "NA()")
                            {
                                cell.Formula = "";
                            }
                            else
                            {
                                cell.Formula = formula.Formula;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                }
            }
            sp.LoadFormulas(true);

            foreach (JZSheet sheet in doc.Sheets)
            {
                SheetView view = GetSheetByID(sp, sheet.ID);
                if (view == null)
                {
                    continue;
                }
                foreach (JZCell dataCell in sheet.Cells)
                {
                    Cell cell = view.Cells[dataCell.Name];
                    if (cell != null)
                    {
                        if (String.IsNullOrEmpty(cell.Formula))
                        {
                            continue;
                        }
                        IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                        if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "图片")
                        {
                            continue;
                        }
                        else if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "数字")
                        {
                            if (cell.Value != null)
                            {
                                Decimal d;
                                if (Decimal.TryParse(cell.Value.ToString().Trim(' ', '\r', '\n'), out d))
                                {
                                    dataCell.Value = d;
                                }
                                else
                                {
                                    dataCell.Value = null;
                                }
                            }
                        }
                        else
                        {
                            dataCell.Value = cell.Value;
                        }
                        if (dataCell.Value != null && dataCell.Value is String)
                        {
                            dataCell.Value = dataCell.Value.ToString().Trim(' ', '\r', '\n');
                        }
                    }
                }
            }
        }

        private class ThreadParameter
        {
            public string QName;
        }
    }
}
