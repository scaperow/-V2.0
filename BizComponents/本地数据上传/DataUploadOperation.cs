using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Yqun.Services;
using System.Collections;

namespace BizComponents
{
    public class DataUploadOperation
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void UpdateData()
        {
            //外网可用的情况下
            if (Internet.IsWanAlive())
            {
                logger.Info("开始扫描本地资料数据......");

                Dictionary<string, string> ModelList = new Dictionary<string, string>();
                Dictionary<string, int> ModelFinishList = new Dictionary<string, int>();
                Dictionary<string, int> ModelErrorList = new Dictionary<string, int>();

                int FinishCount = 0, ErrorCount = 0;

                StringBuilder sql_Models = new StringBuilder();
                sql_Models.Append("select ID,Description,Sheets,ExtentSheet from sys_biz_Module");

                DataTable Data_ModelIndex = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "GetDataTable", new object[] { sql_Models.ToString() }) as DataTable;
                if (Data_ModelIndex != null)
                {
                    logger.Info(string.Format("获取本地模板列表完成，共 {0} 个试验模板。", Data_ModelIndex.Rows.Count));

                    foreach (DataRow Row in Data_ModelIndex.Rows)
                    {
                        List<List<string>> DataGroups = new List<List<string>>();

                        String ModelIndex = Row["ID"].ToString();
                        String ModelDescription = Row["Description"].ToString();
                        String ModelSheets = Row["Sheets"].ToString();
                        String ModelExtentSheet = Row["ExtentSheet"].ToString();
                        String ExtentDataSchema = string.Concat("biz_norm_extent_", ModelIndex);

                        ModelList.Add(ModelIndex, ModelDescription);
                        ModelFinishList.Add(ModelIndex, 0);
                        ModelErrorList.Add(ModelIndex, 0);

                        StringBuilder sql_DataIndex = new StringBuilder();
                        sql_DataIndex.Append("select ID from [");
                        sql_DataIndex.Append(ExtentDataSchema);
                        sql_DataIndex.Append("]");

                        DataTable Data_Index = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "GetDataTable", new object[] { sql_DataIndex.ToString() }) as DataTable;
                        if (Data_Index != null)
                        {
                            logger.Info(string.Format("[{0}]获取资料数据完成，共 {1} 条资料。", ModelDescription, Data_Index.Rows.Count));

                            List<string> DataGroup = new List<string>();
                            foreach (DataRow ItemRow in Data_Index.Rows)
                            {
                                String DataIndex = ItemRow["ID"].ToString();
                                DataGroup.Add(DataIndex);

                                if (DataGroup.Count % 5 == 0)
                                {
                                    DataGroups.Add(DataGroup);
                                    DataGroup = new List<string>();
                                }
                            }

                            if (DataGroup.Count > 0)
                                DataGroups.Add(DataGroup);

                            logger.Info(string.Format("[{0}]全部资料分为 {1} 个包。", ModelDescription, DataGroups.Count));
                        }
                        else
                        {
                            logger.Info(string.Format("[{0}]获取资料数据失败！", ModelDescription));
                        }

                        foreach (List<string> DataGroup in DataGroups)
                        {
                            int index = DataGroups.IndexOf(DataGroup);
                            logger.Info(string.Format("[{0}]开始打包第 {1} 个包......", ModelDescription, index + 1));

                            StringBuilder sql_select;
                            List<string> sql_Commands = new List<string>();
                            string[] ArraySheet = ModelSheets.Split(',');
                            foreach (string Sheet in ArraySheet)
                            {
                                sql_select = new StringBuilder();
                                sql_select.Append("select * from [");
                                sql_select.Append(Sheet);
                                sql_select.Append("] where ID in ('");
                                sql_select.Append(string.Join("','", DataGroup.ToArray()));
                                sql_select.Append("')");

                                sql_Commands.Add(sql_select.ToString());
                            }

                            if (ExtentDataSchema != "")
                            {
                                sql_select = new StringBuilder();
                                sql_select.Append("select * from [");
                                sql_select.Append(ExtentDataSchema);
                                sql_select.Append("] where ID in ('");
                                sql_select.Append(string.Join("','", DataGroup.ToArray()));
                                sql_select.Append("')");

                                sql_Commands.Add(sql_select.ToString());
                            }

                            sql_select = new StringBuilder();
                            sql_select.Append("select * from sys_biz_reminder_evaluateData ");
                            sql_select.Append(" where ID in ('");
                            sql_select.Append(string.Join("','", DataGroup.ToArray()));
                            sql_select.Append("')");

                            sql_Commands.Add(sql_select.ToString());

                            sql_select = new StringBuilder();
                            sql_select.Append("select * from sys_biz_reminder_stadiumData ");
                            sql_select.Append(" where DataID in ('");
                            sql_select.Append(string.Join("','", DataGroup.ToArray()));
                            sql_select.Append("')");

                            sql_Commands.Add(sql_select.ToString());

                            sql_select = new StringBuilder();
                            sql_select.Append("select * from sys_biz_DataUpload ");
                            sql_select.Append(" where ID in ('");
                            sql_select.Append(string.Join("','", DataGroup.ToArray()));
                            sql_select.Append("')");

                            sql_Commands.Add(sql_select.ToString());

                            DataSet Data = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "GetDataSet", new object[] { sql_Commands.ToArray() }) as DataSet;
                            if (Data != null)
                            {
                                byte[] bytes = Yqun.Common.Encoder.Serialize.SerializeToBytes(Data);
                                logger.Info(string.Format("[{0}]获取第 {1} 个包的数据成功,大小为 {1}。", ModelDescription, index + 1, bytes.Length));

                                object r = Agent.CallService("Yqun.BO.BusinessManager.dll", "Update", new object[] { Data });
                                Boolean Result = (Convert.ToInt32(r) == 1);
                                if (Result)
                                {
                                    ModelFinishList[ModelIndex] = ModelFinishList[ModelIndex] + Data.Tables[0].Rows.Count;
                                    FinishCount = FinishCount + Data.Tables[0].Rows.Count;

                                    logger.Info(string.Format("[{0}]更新第 {1} 个包的数据到服务器成功。", ModelDescription, index + 1));

                                    foreach (DataTable Table in Data.Tables)
                                    {
                                        foreach (DataRow ItemRow in Table.Rows)
                                        {
                                            ItemRow.Delete();
                                        }
                                    }

                                    r = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "Update", new object[] { Data });
                                    Boolean bResult = (Convert.ToInt32(r) == 1);
                                    logger.Info(string.Format("[{0}]{1}", ModelDescription, bResult));
                                }
                                else
                                {
                                    ModelErrorList[ModelIndex] = ModelErrorList[ModelIndex] + Data.Tables[0].Rows.Count;
                                    ErrorCount = ErrorCount + Data.Tables[0].Rows.Count;

                                    logger.Info(string.Format("[{0}]更新第 {1} 个包的数据到服务器失败。", ModelDescription, index + 1));
                                }
                            }
                        }
                    }
                }
                else
                {
                    logger.Info("获得本地模板列表失败！");
                }

                logger.Info(string.Format("扫描本地资料数据完毕,成功上传 {0} 条数据，失败 {1} 条数据。",FinishCount, ErrorCount));
                logger.Info("资料上传清单如下：");
                foreach (String key in ModelList.Keys)
                {
                    logger.Info(string.Format("[{0}]成功：{1} 失败：{2}", ModelList[key], ModelFinishList[key], ModelErrorList[key]));
                }
            }
        }
    }
}
