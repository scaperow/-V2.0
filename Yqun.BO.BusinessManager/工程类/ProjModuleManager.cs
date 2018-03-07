using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Windows.Forms;
using System.Data;
using Yqun.Data.DataBase;
using Yqun.Permissions.Common;
using Yqun.Permissions.Runtime;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class ProjModuleManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ProjectCatlogManager ProjectCatlogManager = new ProjectCatlogManager();

        public List<QueryInfo> QueryModuleInfo(string FolderCode)
        {
            List<QueryInfo> ModuleList = new List<QueryInfo>();

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0      2013-10-17
            Sql_Select.Append("select a.RalationID as ModuleID,a.NodeCode as Code,b.Description from sys_engs_Tree as a inner join sys_biz_Module as b on a.RalationID = b.ID and  a.Scdel=0 and a.NodeCode like '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("[0-9]%'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    QueryInfo ModuleInfo = new QueryInfo();
                    ModuleInfo.ModuleId = Row["ModuleID"].ToString();
                    ModuleInfo.ModuleCode = Row["Code"].ToString();
                    ModuleInfo.ModuleDescription = Row["Description"].ToString();
                    ModuleList.Add(ModuleInfo);
                }
            }

            return ModuleList;
        }

        public Boolean HasData(string ModuleCode, String ModuleIndex)
        {
            List<string> sql_Commands = new List<string>();
            List<string> SheetList = ProjectCatlogManager.GetModuleTables(ModuleCode);
            foreach (String SheetName in SheetList)
            {
                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select count(*) from [");
                sql_select.Append(SheetName);
                sql_select.Append("] where scpt like '");
                sql_select.Append(ModuleCode);
                sql_select.Append("%'");

                sql_Commands.Add(sql_select.ToString());
            }

            sql_Commands.Add("select count(*) from [biz_norm_extent_" + ModuleIndex + "] where scpt like '" + ModuleCode + "%'");

            foreach (string s in sql_Commands)
            {
                //logger.Error(s);
            }

            Boolean Result = false;
            int Count = 0;
            object[] rArray = ExcuteScalars(sql_Commands.ToArray());
            foreach (object o in rArray)
            {
                if (int.TryParse(o.ToString(), out Count) && Count > 0)
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        public Boolean DeleteModule(string ModuleCode)
        {
            List<string> SheetList = ProjectCatlogManager.GetModuleTables(ModuleCode);

            Boolean Result = false;

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                List<string> sql_Commands = new List<string>();

                //工程结构树
                //StringBuilder Sql_Delete = new StringBuilder();
                //Sql_Delete.Append("Delete From sys_engs_Tree Where NodeCode = '");
                //Sql_Delete.Append(ModuleCode);
                //Sql_Delete.Append("'");
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                StringBuilder Sql_Delete = new StringBuilder();
                Sql_Delete.Append("Update sys_engs_Tree Set Scts_1=Getdate(),Scdel=1 Where NodeCode = '");
                Sql_Delete.Append(ModuleCode);
                Sql_Delete.Append("'");

                sql_Commands.Add(Sql_Delete.ToString());

                //处理模板视图表
                Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_moduleview Where ModuleCode like '");
                //Sql_Delete.Append(ModuleCode);
                //Sql_Delete.Append("%'");
                Sql_Delete.Append("Update sys_moduleview Set Scts_1=Getdate(),Scdel=1 Where ModuleCode like '");
                Sql_Delete.Append(ModuleCode);
                Sql_Delete.Append("%'");

                sql_Commands.Add(Sql_Delete.ToString());

                //报告上传标记
                StringBuilder sql_Delete3 = new StringBuilder();
                sql_Delete3.Append("delete from sys_biz_DataUpload where SCPT='");
                sql_Delete3.Append(ModuleCode);
                sql_Delete3.Append("'");
                sql_Commands.Add(sql_Delete3.ToString());

                //报告不合格检测项
                StringBuilder sql_Delete4 = new StringBuilder();
                sql_Delete4.Append("delete from sys_biz_reminder_evaluateData where ModelCode='");
                sql_Delete4.Append(ModuleCode);
                sql_Delete4.Append("'");
                sql_Commands.Add(sql_Delete4.ToString());

                //龄期提醒数据
                StringBuilder sql_Delete5 = new StringBuilder();
                sql_Delete5.Append("delete from sys_biz_reminder_stadiumData where ModelCode='");
                sql_Delete5.Append(ModuleCode);
                sql_Delete5.Append("'");
                sql_Commands.Add(sql_Delete5.ToString());

                object r = ExcuteCommands(sql_Commands.ToArray(), Transaction);
                int[] ints = (int[])r;
                for (int i = 0; i < ints.Length; i++)
                {
                    if (i != 0)
                    {
                        Result = Result && (Convert.ToInt32(ints[i]) == 1);
                    }
                    else
                    {
                        Result = (Convert.ToInt32(ints[i]) == 1);
                    }
                }

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

        public Boolean HaveModuleInfo(string TreeCode, string ModuleId)
        {
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0      2013-10-17
            Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and NodeCode like'");
            Sql_Select.Append(TreeCode);
            Sql_Select.Append("%'");
            Sql_Select.Append(" and ");
            Sql_Select.Append(" RalationID = '");
            Sql_Select.Append(ModuleId);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }
        
        public Boolean SaveTemlateResult(string ModuleID, string ModuleCode)
        {
            Boolean Result = false;

            //工程结构树
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0      2013-10-17
            Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID,Scts_1 From sys_engs_Tree Where Scdel=0 and RalationID='");
            Sql_Select.Append(ModuleCode);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Guid.NewGuid().ToString();
                Row["NodeCode"] = ModuleCode;
                Row["NodeType"] = "@module";
                Row["RalationID"] = ModuleID;

                Row["Scts_1"] = DateTime.Now.ToString();
                Data.Rows.Add(Row);
            }
            try
            {
                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }
            return Result;

        }
    }
}
