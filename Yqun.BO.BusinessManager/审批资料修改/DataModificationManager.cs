using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Data.DataBase;
using System.Threading;
using System.IO;
using System.Xml;
using Yqun.Common.ContextCache;

namespace Yqun.BO.BusinessManager
{
    public class DataModificationManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable InitDataModificationList(String[] TestRoomCode)
        {
            if (TestRoomCode == null || TestRoomCode.Length == 0)
                return new DataTable();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,DataID,ModelIndex,ModelCode, CompanyName as 单位名称,TestRoomName as 试验室名称,ModelName as 模板名称,SponsorPerson as 申请者,SponsorDate as 申请日期,Caption as 内容,Reason as 原因 from sys_biz_DataModification where State='已提交'");

            List<string> subExpressions = new List<string>();
            foreach (String code in TestRoomCode)
            {
                subExpressions.Add(string.Format("ModelCode like '{0}____'", code));
            }
            sql_select.Append(string.Concat(" and (", string.Join(" or ", subExpressions.ToArray()), ")"));

            return GetDataTable(sql_select.ToString());
        }

        public DataTable InitDataModificationList(String segment, String company, String testroom, DateTime start, DateTime end, String status, String content, String user)
        {
            String sql = String.Format(@"select ID,DataID,ModelIndex,ModelCode,State as 状态,Segment as 标段,CompanyName as 单位名称,TestRoomName as 试验室名称,ModelName as 模板名称,SponsorPerson as 申请者,SponsorDate as 申请日期,Caption as 内容,Reason as 原因,ProcessReason as 处理意见 from sys_biz_DataModification where 
                 SponsorDate>='{0}' AND SponsorDate<'{1}'",
                start.ToString("yyyy-MM-dd"),
                end.AddDays(1).ToString("yyyy-MM-dd"));

            if (testroom != "")
            {
                sql += " AND LEFT(ModelCode,16)='" + testroom + "' ";
            }
            else
            {
                TestRoomCodeHelper trch = new TestRoomCodeHelper();
                List<String> list = trch.GetValidTestRoomCodeList();
                List<String> real1 = new List<string>();
                List<String> real2 = new List<string>();
                if (!String.IsNullOrEmpty(company))
                {
                    foreach (var item in list)
                    {
                        if (item.StartsWith(company))
                        {
                            real1.Add(item);
                        }
                    }
                }
                else
                {
                    real1.AddRange(list);
                }


                if (!String.IsNullOrEmpty(segment))
                {
                    foreach (var item in real1)
                    {
                        if (item.StartsWith(segment))
                        {
                            real2.Add(item);
                        }
                    }
                }
                else
                {
                    real2.AddRange(real1);
                }

                if (real2.Count == 0)
                {
                    return null;
                }
                sql += " AND LEFT(ModelCode,16) in ('" + String.Join("','", real2.ToArray()) + "') ";
            }
            if (user != "")
            {
                sql += " AND SponsorPerson like '%" + user + "%' ";
            }
            if (content != "")
            {
                sql += " AND Caption like '%" + content + "%' ";
            }

            sql += " ORDER BY SponsorDate DESC";
            DataTable Data = GetDataTable(sql);
            return Data;
        }

        public Boolean HaveDataModificationInfo(String DataID)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select count(*) from sys_biz_DataModification as a,");
            sql_select.Append("(select max(scts) as scts from sys_biz_DataModification where DataID='");
            sql_select.Append(DataID);
            sql_select.Append("') as b where a.scts = b.scts and a.State = '已提交'");

            int r = Convert.ToInt32(ExcuteScalar(sql_select.ToString()));
            return (r >= 1);
        }


        public DataTable HaveDataModificationInfoByID(String ID)
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append(@"SELECT a.ID,a.DataID,b.ModuleID,a.State,c.标段名称,c.单位名称,c.试验室名称,
            d.Name AS 模板名称,a.RequestBy AS 申请者,a.RequestTime AS 申请日期,a.Caption,
            a.Reason,a.ProcessReason,a.ApprovePerson,a.ApproveTime,a.IsRequestStadium FROM dbo.sys_request_change a
            JOIN dbo.sys_document b ON a.DataID=b.ID
            JOIN dbo.v_bs_codeName c ON b.TestRoomCode = c.试验室编码
            JOIN dbo.sys_module d ON d.ID = b.ModuleID Where a.ID='");
            sql_select.Append(ID);
            sql_select.Append("' ");
            DataTable Data = GetDataTable(sql_select.ToString());
            return Data;
        }

        public Boolean NewDataModificationInfo(DataModificationInfo Info)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,Scts,DataID,ModuleID,TestRoomCode,Segment,CompanyName,TestRoomName,ModuleName,SponsorPerson,SponsorDate,Caption,Reason,ApprovePerson,ApproveDate,State from sys_biz_DataModification where ID='");
            sql_select.Append(Info.Index);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Info.Index;
                Row["Scts"] = DateTime.Now.ToString();
                Row["DataID"] = Info.DataID;
                Row["ModuleID"] = Info.ModuleID;
                Row["TestRoomCode"] = Info.TestRoomCode;
                Row["CompanyName"] = Info.CompanyName;
                Row["TestRoomName"] = Info.TestRoomName;
                Row["ModuleName"] = Info.ModelName;
                Row["SponsorPerson"] = Info.SponsorPerson;
                Row["SponsorDate"] = DateTime.Now.ToString();
                Row["Caption"] = Info.Caption;
                Row["Reason"] = Info.Reason;
                Row["State"] = Info.State;
                Row["Segment"] = Info.Segment;
                Data.Rows.Add(Row);
            }

            try
            {
                int r = Update(Data);
                Result = (r == 1);
            }
            catch
            {
            }

            return Result;
        }

        public Boolean UpdateDataModificationInfoAndStadium(String[] IDs, String ApprovePerson, String State, String processReason, int IsRequestStadium)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,DataID,ApprovePerson,ApproveTime,State,ProcessReason from sys_request_change where ID='");
            sql_select.Append(IDs[0]);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            string DataID = string.Empty;
            StadiumHelper stadium = new StadiumHelper();
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    Row["ApprovePerson"] = ApprovePerson;
                    Row["ApproveTime"] = DateTime.Now;
                    Row["State"] = State;
                    Row["ProcessReason"] = processReason;
                    DataID = Row["DataID"].ToString();
                    stadium.ResetStadiumToTodayByDataID(DataID);
                }
            }

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            try
            {
                int r = Update(Data, Transaction);
                Result = (r == 1);

                //对于通过审核的将Document的Status设置为2
                Result = Result && UpdateDocumentStatus(Data, Transaction);

                if (Result)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                }
            }
            catch
            {
                Transaction.Rollback();
            }

            return Result;
        }
        public Boolean UpdateDataModificationInfo(String[] IDs, String ApprovePerson, String State, String processReason)
        {
            return UpdateDataModificationInfoAndStadium(IDs, ApprovePerson, State, processReason, 0);
        }

        public Boolean UpdateDocumentStatus(DataTable Data, Transaction Transaction)
        {
            Boolean Result = true;

            String sql = "";
            int r;
            try
            {
                foreach (DataRow Row in Data.Rows)
                {
                    if (Row["State"].ToString() == "通过")
                    {
                        sql = "UPDATE dbo.sys_document SET Status=2 WHERE ID='" + Row["DataID"] + "'";
                        r = ExcuteCommand(sql, Transaction);
                        Result = Result && (r == 1);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("UpdateDocumentStatus error: " + e.Message);
            }
            return Result;
        }

        public void saveDataModificationLog(String ModelIndex, DataSet dataset)
        {
            ThreadParameter p = new ThreadParameter();
            p.Data = dataset;
            p.ModelIndex = ModelIndex;
            ApplicationContext context = ApplicationContext.Current;
            p.UserName = context.UserName;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {

            ThreadParameter p = paremeter as ThreadParameter;
            DataSet dataset = p.Data;
            String modelIndex = p.ModelIndex;
            if (dataset == null)
                return;

            List<ModifyItem> modifyList = new List<ModifyItem>();
            try
            {

                foreach (DataTable Table in dataset.Tables)
                {
                    foreach (DataRow Row in Table.Rows)
                    {
                        foreach (DataColumn Column in Table.Columns)
                        {
                            String originalValue = Convert.ToString(Row[Column.ColumnName, DataRowVersion.Original]);
                            String currentValue = Convert.ToString(Row[Column.ColumnName, DataRowVersion.Current]);
                            if (originalValue == "" || originalValue == "/")
                            {
                                continue;
                            }
                            if (Column.ColumnName.StartsWith("col_norm_") && String.Compare(originalValue, currentValue, true) != 0)
                            {
                                ModifyItem item = new ModifyItem()
                                {
                                    CellPosition = Column.ColumnName.Replace("col_norm_", ""),
                                    SheetName = Table.TableName.Replace("[biz_norm_", "").Replace("]", ""),
                                    OriginalValue = originalValue.Replace("'", "''"),
                                    CurrentValue = currentValue.Replace("'", "''")
                                };
                                modifyList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("changes log modify:" + ex.Message);
            }

            try
            {
                if (modifyList.Count == 0)
                {
                    return;
                }
                String modelName = GetModelNameByID(modelIndex);
                String reportNumber = GetReportNumberByModelID(modelIndex, dataset);
                DataTable tb = dataset.Tables["[biz_norm_extent_" + modelIndex + "]"];
                if (tb == null || tb.Rows.Count <= 0)
                {
                    logger.Error(String.Format("传入数据中无[biz_norm_extent_{0}]", modelIndex));
                    return;
                }

                String dataID = tb.Rows[0]["ID"].ToString();
                String modelCode = tb.Rows[0]["SCPT"].ToString();
                String reportName = string.Empty;
                if (tb.Columns.Contains("DataName"))
                {
                    reportName = tb.Rows[0]["DataName"].ToString();
                }
                else
                {
                    reportName = GetModelNameByID(p.ModelIndex);
                }
                String modifyItems = Newtonsoft.Json.JsonConvert.SerializeObject(modifyList);

                String relateApplyID = "";
                if (tb.Rows[0]["SCCS"] != DBNull.Value)
                {
                    relateApplyID = tb.Rows[0]["SCCS"].ToString();
                }

                //logger.Error("SCCS= " + relateApplyID);

                String segment = GetDepartmentInfo("sys_engs_SectionInfo", modelCode.Substring(0, 8));
                String company = GetDepartmentInfo("sys_engs_CompanyInfo", modelCode.Substring(0, 12));
                String testRoom = GetDepartmentInfo("sys_engs_ItemInfo", modelCode.Substring(0, 16));

                String insertSql = String.Format(@"INSERT INTO dbo.sys_operatelog
                            ( modifiedby ,
                              modifiedDate ,
                              optType,
                              modelIndex ,
                              modelCode ,
                              dataID ,
                              segmentName ,
                              companyName ,
                              testRoom ,
                              modelName ,
                              reportName ,
                              reportNumber ,
                              modifyItem ,
                              comment,
                              testRoomCode
                            )
                    VALUES  ( '{0}','{1}','修改','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}' )",
                              p.UserName,
                              DateTime.Now,
                              modelIndex,
                              modelCode,
                              dataID,
                              segment,
                              company,
                              testRoom,
                              modelName,
                              reportName,
                              reportNumber,
                              modifyItems,
                              relateApplyID,
                              modelCode.Substring(0, 16));
                ExcuteCommand(insertSql);

                //将申请修改字段SCCS，变更为空 
                String sql = "update [biz_norm_extent_" + modelIndex + "] set SCCS=null where ID='" + dataID + "' ";
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                logger.Error("changes log big error:" + ex.Message);
            }
        }

        public String GetDeleteLogSql(String ModelIndex, DataSet dataset)
        {
            String sql = "";
            ApplicationContext context = ApplicationContext.Current;

            try
            {
                String modelName = GetModelNameByID(ModelIndex);
                String reportNumber = GetReportNumberByModelID(ModelIndex, dataset);
                DataTable tb = dataset.Tables["[biz_norm_extent_" + ModelIndex + "]"];
                if (tb == null || tb.Rows.Count <= 0)
                {
                    logger.Error(String.Format("传入数据中无[biz_norm_extent_{0}]", ModelIndex));
                    return "";
                }

                String dataID = tb.Rows[0]["ID"].ToString();
                String modelCode = tb.Rows[0]["SCPT"].ToString();
                String reportName = string.Empty;
                if (tb.Columns.Contains("DataName"))
                {
                    reportName = tb.Rows[0]["DataName"].ToString();
                }
                else
                {
                    reportName = GetModelNameByID(ModelIndex);
                }

                String segment = GetDepartmentInfo("sys_engs_SectionInfo", modelCode.Substring(0, 8));
                String company = GetDepartmentInfo("sys_engs_CompanyInfo", modelCode.Substring(0, 12));
                String testRoom = GetDepartmentInfo("sys_engs_ItemInfo", modelCode.Substring(0, 16));

                String modifyItems = Newtonsoft.Json.JsonConvert.SerializeObject(dataset);
                modifyItems = modifyItems.Replace("'", "''");
                sql = String.Format(@"INSERT INTO dbo.sys_operatelog
                            ( modifiedby ,
                              modifiedDate ,
                              optType,
                              modelIndex ,
                              modelCode ,
                              dataID ,
                              segmentName ,
                              companyName ,
                              testRoom ,
                              modelName ,
                              reportName ,
                              reportNumber ,
                              modifyItem ,
                              comment,
                              testRoomCode
                            )
                    VALUES  ( '{0}','{1}','删除','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                              context.UserName,
                              DateTime.Now,
                              ModelIndex,
                              modelCode,
                              dataID,
                              segment,
                              company,
                              testRoom,
                              modelName,
                              reportName,
                              reportNumber,
                              modifyItems,
                              "",
                              modelCode.Substring(0, 16));
            }
            catch (Exception ex)
            {
                logger.Error("get delete sql error:" + ex.Message);
            }
            return sql;
        }

        private String GetModelNameByID(String id)
        {
            String sql = String.Format("SELECT Description FROM dbo.sys_biz_Module WHERE ID='{0}'", id);
            DataTable tb = GetDataTable(sql);
            String result = "";
            if (tb != null && tb.Rows.Count == 1)
            {
                if (tb.Rows.Count > 0)
                {
                    result = tb.Rows[0][0].ToString();
                }
            }
            return result;
        }

        private String GetReportNumberByModelID(String id, DataSet ds)
        {
            String reportNumber = "";
            String sql = String.Format("SELECT TableName, Contents FROM  sys_moduleview WHERE ModuleID = '{0}' AND Description='报告编号' ", id);
            DataTable tb = GetDataTable(sql);
            if (tb != null && tb.Rows.Count > 0)
            {
                String tableName = "[" + tb.Rows[0]["TableName"] + "]";
                String columnName = tb.Rows[0]["Contents"].ToString();
                DataTable sheet = ds.Tables[tableName];
                if (sheet != null)
                {
                    if (sheet.Rows.Count > 0)
                    {
                        reportNumber = sheet.Rows[0][columnName].ToString();
                    }
                }
            }
            else
            {
                logger.Error("未在台账中设置报告编号, modelID=" + id);
            }
            return reportNumber;
        }

        private String GetDepartmentInfo(String tableName, String modelCode)
        {
            String sql = String.Format("select b.Description from sys_engs_Tree as a JOIN {0} b on a.RalationID = b.ID and a.nodeCode='{1}'",
                tableName, modelCode);
            DataTable tb = GetDataTable(sql);
            String result = "";
            if (tb != null && tb.Rows.Count == 1)
            {
                if (tb.Rows.Count > 0)
                {
                    result = tb.Rows[0][0].ToString();
                }
            }
            return result;
        }

        private class ThreadParameter
        {
            public DataSet Data;
            public String ModelIndex;
            public String UserName;
        }

        [Serializable]
        public class ModifyItem
        {
            public String SheetName { get; set; }
            public String CellPosition { get; set; }
            public String OriginalValue { get; set; }
            public String CurrentValue { get; set; }
        }
    }
}
