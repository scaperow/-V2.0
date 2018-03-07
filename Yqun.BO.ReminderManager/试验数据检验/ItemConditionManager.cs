using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Yqun.BO.ReminderManager
{
    public class ItemConditionManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Boolean UpdateItemConditions(String DataIndex, String SCTS, String ModelCode, String ModelIndex, String ReportName, String ReportNumber, String ReportDate, String InvalidItems)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID,SCTS,ModelCode,ModelIndex,ReportName,ReportNumber,ReportDate,F_InvalidItem from sys_biz_reminder_evaluateData where ID='");
            sql_select.Append(DataIndex);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                DataRow Row = null;

                try
                {
                    if (Data.Rows.Count > 0)
                    {
                        Row = Data.Rows[0];
                    }
                    else
                    {
                        Row = Data.NewRow();
                        Data.Rows.Add(Row);
                    }
                    Row["ID"] = DataIndex;
                    Row["SCTS"] = DateTime.Now;
                    Row["ModelCode"] = ModelCode;
                    Row["ModelIndex"] = ModelIndex;
                    Row["ReportName"] = ReportName;
                    Row["ReportNumber"] = ReportNumber;
                    if (String.IsNullOrEmpty(ReportDate))
                    {
                        Row["ReportDate"] = null;
                    }
                    else
                    {
                        Row["ReportDate"] = ReportDate;
                    }
                    Row["F_InvalidItem"] = InvalidItems;
                    int r = Update(Data);
                    Result = (r == 1);
                }
                catch (Exception ex)
                {
                    logger.Error("ItemConditionManager.UpdateItemConditions:" + ex.Message);
                }
            }

            return Result;
        }

        public Boolean DeleteItemConditions(String[] DataIndex)
        {
            if (DataIndex.Length == 0)
                return true;

            Boolean Result = false;

            StringBuilder sql_delete = new StringBuilder();
            sql_delete.Append("delete from sys_biz_reminder_evaluateData where ID in ('");
            sql_delete.Append(string.Join("','", DataIndex));
            sql_delete.Append("')");

            try
            {
                int r = ExcuteCommand(sql_delete.ToString());
                Result = (r == 1);
            }
            catch
            {
            }

            return Result;
        }

        public void QualifyInvalidReport(String DataIndex)
        {
            String sql = String.Format("select ID from sys_biz_reminder_evaluateData where ID='{0}' and AdditionalQualified=0", DataIndex);

            DataTable Data = GetDataTable(sql);
            if (Data != null)
            {
                if (Data.Rows.Count > 0)
                {
                    String updateSql = String.Format("update sys_biz_reminder_evaluateData set AdditionalQualified=1, QualifiedTime=getdate() where ID='{0}'", DataIndex);
                    ExcuteCommand(updateSql);
                }
                else
                {
                    String updateSql = String.Format("update sys_biz_reminder_evaluateData set AdditionalQualified=0, QualifiedTime=getdate() where ID='{0}'", DataIndex);
                    ExcuteCommand(updateSql);
                }
            }
        }

        public void SyncInvalidReport(Guid dataID, String invalidString)
        {
            String sql = String.Format("select ID,AdditionalQualified from sys_invalid_document where ID='{0}' ", dataID);

            DataTable Data = GetDataTable(sql);
            if (Data != null)
            {
                if (Data.Rows.Count > 0)
                {
                    String updateSql = string.Empty;
                    if (invalidString != "")
                    {
                        updateSql = String.Format(@"update sys_invalid_document set AdditionalQualified=0,LastEditedTime=getdate(), F_InvalidItem='" + invalidString.Replace("'", "''") + @"',
WTBH=b.WTBH,
BGBH=b.BGBH,
BGRQ=b.BGRQ,
ModuleID=b.ModuleID,
Status=b.Status,
TestRoomCode=b.TestRoomCode FROM dbo.sys_invalid_document a,dbo.sys_document b WHERE a.ID=b.ID AND a.ID='{0}'", dataID);
                    }
                    else
                    {
                        int AdditionalQualified = int.Parse(Data.Rows[0]["AdditionalQualified"].ToString());
                        if (AdditionalQualified == 0)
                        {//以前不合格
                            updateSql = String.Format(@"update sys_invalid_document set AdditionalQualified=1,LastEditedTime=getdate(), QualifiedTime=getdate(),
WTBH=b.WTBH,
BGBH=b.BGBH,
BGRQ=b.BGRQ,
ModuleID=b.ModuleID,
Status=b.Status,
TestRoomCode=b.TestRoomCode FROM dbo.sys_invalid_document a,dbo.sys_document b WHERE a.ID=b.ID AND a.ID='{0}'", dataID);
                        }
                        else
                        {//已经是合格的
                            updateSql = String.Format(@"update sys_invalid_document set LastEditedTime=getdate(),
WTBH=b.WTBH,
BGBH=b.BGBH,
BGRQ=b.BGRQ,
ModuleID=b.ModuleID,
Status=b.Status,
TestRoomCode=b.TestRoomCode FROM dbo.sys_invalid_document a,dbo.sys_document b WHERE a.ID=b.ID AND a.ID='{0}'", dataID);
                        }

                    }
                    ExcuteCommand(updateSql);
                }
                else
                {
                    if (invalidString != "")
                    {
                        String insertSql = String.Format(@"INSERT INTO dbo.sys_invalid_document
      ( ID ,F_InvalidItem ,ModuleID ,TestRoomCode ,WTBH,BGBH,BGRQ,Status)
 SELECT ID,'{1}', ModuleID ,TestRoomCode ,WTBH,BGBH,BGRQ ,Status
 FROM dbo.sys_document WHERE id='{0}'", dataID, invalidString);
                        ExcuteCommand(insertSql);
                    }
                }
            }
        }
    }
}
