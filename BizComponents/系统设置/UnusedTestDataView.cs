using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizCommon;
using Yqun.Interfaces;
using BizComponents;
using Yqun.Bases;
using System.IO;
using System.ServiceModel;
using TransferServiceCommon;
using System.Collections;

namespace BizComponents
{
    public partial class UnusedTestDataView : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        System.Timers.Timer tmManual = new System.Timers.Timer();
        int EnableTime = 60;
        public UnusedTestDataView()
        {
            InitializeComponent();
        }

        private void UnusedTestDataView_Load(object sender, EventArgs e)
        {

            FpSpread_Info.Rows.Count = 0;
            FpSpread_Info.Columns.Count = 12;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "是否入库";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "是否离线";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "试验日期";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "委托编号";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "试验项目";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "序号";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "试验用户";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "试验结果";
            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "ModuleID";
            FpSpread_Info.ColumnHeader.Cells[0, 9].Text = "ID";
            FpSpread_Info.ColumnHeader.Cells[0, 10].Text = "试验室编码";
            FpSpread_Info.ColumnHeader.Cells[0, 11].Text = "机器编码";


            FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            FarPoint.Win.Spread.CellType.NumberCellType number = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
            //number
            FpSpread_Info.Columns[2].CellType = datetime;
            FpSpread_Info.Columns[8].Visible = false;
            FpSpread_Info.Columns[9].Visible = false;

            SearchUnusedTestData("!@##@!");

            btnManualUploadMQ.Text = "启动实时队列";
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-2"&&Yqun.Common.ContextCache.ApplicationContext.Current.UserName=="谭利平")
            {
                btnManualUploadMQ.Visible = true;
                btnReloadMQ.Visible = true;
                btnStartMQAll.Visible = true;
            }
            else
            {
                btnManualUploadMQ.Visible = false;
                //btnReloadMQ.Visible = false;
                btnStartMQAll.Visible = false;
            }
            if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode == "-1")
            {
                btnReloadMQ.Visible = true;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string strWTBH = txtWTBH.Text.Trim();
            if (string.IsNullOrEmpty(strWTBH))
            {
                MessageBox.Show("请输入委托编号");
            }
            else
            {
                SearchUnusedTestData(strWTBH);
            }
        }
        private void SearchUnusedTestData(string strWTBH)
        {
            DataTable Data = CaiJiHelperClient.GetUnusedTestData(strWTBH);
            if (Data != null)
            {
                FpSpread_Info.Columns[0].Width = 80;
                FpSpread_Info.Columns[1].Width = 80;
                FpSpread_Info.Columns[2].Width = 120;
                FpSpread_Info.Columns[3].Width = 100;
                FpSpread_Info.Columns[4].Width = 100;
                FpSpread_Info.Columns[5].Width = 40;
                FpSpread_Info.Columns[6].Width = 80;
                FpSpread_Info.Columns[7].Width = 400;
                FpSpread_Info.Columns[8].Width = 100;
                FpSpread_Info.Columns[9].Width = 100;
                FpSpread_Info.Columns[10].Width = 100;
                FpSpread_Info.Columns[11].Width = 120;

                FpSpread_Info.Rows.Count = Data.Rows.Count;

                int i, j;
                string strModuleIDs = string.Empty;

                foreach (DataRow Row in Data.Rows)
                {
                    strModuleIDs += "'" + Row["ModuleID"].ToString() + "',";
                }
                if (strModuleIDs.Length > 0)
                {
                    strModuleIDs = strModuleIDs.Substring(0, strModuleIDs.Length - 1);
                }
                DataTable dtSheet = ModuleHelperClient.GetSheetIDAndName(strModuleIDs);
                foreach (System.Data.DataColumn Column in Data.Columns)
                {

                    i = Data.Columns.IndexOf(Column);

                    FpSpread_Info.Columns[i].VerticalAlignment = CellVerticalAlignment.Center;
                    FpSpread_Info.Columns[i].Label = Column.ColumnName;

                    foreach (DataRow Row in Data.Rows)
                    {
                        j = Data.Rows.IndexOf(Row);
                        if (i == 7)//解析试验结果
                        {
                            string strTestData = Row[Column.ColumnName].ToString();
                            StringBuilder strTestDataName = new StringBuilder();
                            try
                            {
                                List<JZTestCell> lstJZTC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(strTestData);
                                string strSheetName = string.Empty;
                                bool bHaveSheet = false; ;
                                foreach (JZTestCell tcItem in lstJZTC)
                                {
                                    if (bHaveSheet == false)
                                    {
                                        bHaveSheet = true;
                                        strSheetName = GetSheetName(dtSheet, tcItem.SheetID.ToString());
                                        if (string.IsNullOrEmpty(strSheetName))
                                        {
                                            strTestDataName.Append(tcItem.SheetID + "|");
                                        }
                                        else
                                        {
                                            strTestDataName.Append(strSheetName + "|");
                                        }
                                    }
                                    switch (tcItem.Name)
                                    {
                                        case JZTestEnum.DHBJ:
                                            strTestDataName.Append("断后标距:" + tcItem.Value);
                                            break;
                                        case JZTestEnum.LDZDL:
                                            strTestDataName.Append("拉断最大力:" + tcItem.Value);
                                            break;
                                        case JZTestEnum.PHHZ:
                                            strTestDataName.Append("破坏荷载:" + tcItem.Value);
                                            break;
                                        case JZTestEnum.QFL:
                                            strTestDataName.Append("屈服力:" + tcItem.Value);
                                            break;
                                        default:
                                            break;
                                    }
                                    strTestDataName.Append(";");
                                }
                            }
                            catch (Exception ex)
                            {
                                //logger.Error(string.Format("解析Json出错,TestData:{0},Exception:{1}", strTestData, ex.Message));
                                strTestDataName.Append(strTestData);
                                //continue;
                            }
                            FpSpread_Info.Cells[j, i].Value = strTestDataName.ToString();
                        }
                        else
                        {
                            FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                            FpSpread_Info.Cells[j, i].Value = Row[Column.ColumnName].ToString();
                        }
                        FpSpread_Info.Rows[j].Tag = Row["ID"];
                    }
                }
            }
        }

        private string GetSheetName(DataTable dtSheet, string SheetID)
        {
            string strReturn = string.Empty;
            DataRow[] drArr = dtSheet.Select("ID='" + SheetID + "'");
            if (drArr.Length > 0)
            {
                strReturn = drArr[0]["Name"].ToString();
            }
            return strReturn;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Row Row = FpSpread_Info.ActiveRow;

            if (Row != null && Row.Tag != null)
            {

                Guid testID = new Guid(Row.Tag.ToString());
                CaiJiChartView ccv = new CaiJiChartView(testID);
                ccv.ShowDialog();
            }
        }

        private void ToolStripMenuItem_ShowTestRoomInfo_Click(object sender, EventArgs e)
        {
            Int32 i = FpSpread_Info.ActiveRowIndex;
            string TestRoomCode = FpSpread_Info.Cells[i, 10].Value.ToString();

            ShowTestRoomInfoDlg ccv = new ShowTestRoomInfoDlg(TestRoomCode);
            ccv.ShowDialog();
        }

        private void btnManualUploadMQ_Click(object sender, EventArgs e)
        {
            CaiJiHelperClient.ManualUploadMQ();
            EnableTime = 60;
            btnManualUploadMQ.Enabled = false;
            tmManual.AutoReset = true;
            tmManual.Elapsed += new System.Timers.ElapsedEventHandler(tmManual_Elapsed);
            tmManual.Interval = 1000;
            tmManual.Enabled = true;
            tmManual.Start();
        }

        void tmManual_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strShowMsg = "启动实时队列({0})";
            if (EnableTime > 0)
            {
                btnManualUploadMQ.Text = string.Format(strShowMsg, EnableTime--);
            }
            else
            {
                tmManual.Enabled = false;
                tmManual.Stop();
                btnManualUploadMQ.Enabled = true;
                btnManualUploadMQ.Text = "启动实时队列";
            }
        }

        private void btnReloadMQ_Click(object sender, EventArgs e)
        {
            string strStartTime = txtWTBH.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(strStartTime))
            {
                bool bIsDate = false;
                DateTime dtStartTime = new DateTime();
                bIsDate = DateTime.TryParse(strStartTime, out dtStartTime);

                if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode != "-2" && bIsDate == true)
                {
                    return;
                }
                CaiJiHelperClient.ReloadRabbitMQ(strStartTime);
                MessageBox.Show("重新入库成功");
            }
        }

        private void btnStartMQAll_Click(object sender, EventArgs e)
        {

            string strStartTime = txtWTBH.Text.Trim().Replace(",", "");
            if (!string.IsNullOrEmpty(strStartTime))
            {
                List<Sys_Line> lstLine;
                string strLocalServerIP = "112.124.99.146";
                String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
                string errMsg;
                object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetMQWSLinesByIP",
                    new Object[] { strLocalServerIP });
                if (objLib != null)
                {
                    lstLine = objLib as List<Sys_Line>;


                    foreach (Sys_Line line in lstLine)
                    {
                        ReloadMQ(line.LineIP, line.LinePort, strStartTime, out errMsg);
                    }
                }
                else
                {
                    logger.Info(string.Format("采集数据上传没有需要处理的线路"));
                }
            }
        }

        #region 通用方法

        private bool ReloadMQ(string IPAddress, string Port, string StartTime, out string errMsg)
        {
            bool bSuccess = false;
            try
            {
                String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
                bSuccess = Convert.ToBoolean(CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "ReloadRabbitMQ",
                   new Object[] { StartTime }).ToString());
                errMsg = "";
            }
            catch (Exception ex)
            {
                bSuccess = false;
                errMsg = ex.Message;
            }
            return bSuccess;
        }
        public object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
        {
            object obj = null;
            using (ChannelFactory<ITransferService> channelFactory = new ChannelFactory<ITransferService>("TransferServiceEndPoint", new EndpointAddress(address)))
            {

                ITransferService proxy = channelFactory.CreateChannel();
                using (proxy as IDisposable)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable["assembly_name"] = AssemblyName;
                    hashtable["method_name"] = MethodName;
                    hashtable["method_paremeters"] = Parameters;

                    Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                    Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream);
                    source_stream.Dispose();
                    Stream stream_result = proxy.InvokeMethod(zip_stream);
                    zip_stream.Dispose();
                    Stream ms = ReadMemoryStream(stream_result);
                    stream_result.Dispose();
                    Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                    ms.Dispose();
                    Hashtable Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;

                    obj = Result["return_value"];
                }
            }
            return obj;
        }

        private MemoryStream ReadMemoryStream(Stream Params)
        {
            MemoryStream serviceStream = new MemoryStream();
            byte[] buffer = new byte[10000];
            int bytesRead = 0;
            int byteCount = 0;

            do
            {
                bytesRead = Params.Read(buffer, 0, buffer.Length);
                serviceStream.Write(buffer, 0, bytesRead);

                byteCount = byteCount + bytesRead;
            } while (bytesRead > 0);

            serviceStream.Position = 0;

            return serviceStream;
        }
        #endregion
    }
}
