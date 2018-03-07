using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Permissions.Common;
using Yqun.Permissions.Runtime;
using Yqun.Common.ContextCache;

namespace Yqun.BO.BusinessManager
{
    public class ProjectCatlogManager : BOBase
    {
        public static String TreeID = "6ED9D9CB-117E-4d8c-A63B-0157BD1F9DFD";

        ModuleManager ModuleManager = new ModuleManager();

        public DataTable InitProjectCatlog()
        {
            IAuthPolicy AuthPolicy = AuthManager.GetTreeAuth(TreeID);

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,a.NodeCode,NodeType,RalationID,");
            Sql_Select.Append("(");
            Sql_Select.Append("	select Description from ");
            Sql_Select.Append("	(");
            Sql_Select.Append(" select ID,Description from sys_engs_ProjectInfo");
            Sql_Select.Append(" union");
            Sql_Select.Append(" select ID,Description from sys_engs_CompanyInfo");
            Sql_Select.Append(" union");
            Sql_Select.Append(" select ID,Description from sys_engs_SectionInfo");
            Sql_Select.Append(" union");
            Sql_Select.Append(" select ID,Name as Description from sys_module");
            Sql_Select.Append(" union");
            Sql_Select.Append(" select ID,Description from sys_engs_ItemInfo");
            Sql_Select.Append("	) as b where b.ID = a.RalationID");
            Sql_Select.Append(") as RalationText,c.OrderID ");
            Sql_Select.Append("from sys_engs_Tree as a LEFT JOIN dbo.Sys_Tree c ON a.NodeCode=c.NodeCode");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append(" WHERE a.scdel=0 ");
            Sql_Select.Append("order by c.OrderID");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            DataTable NewData = new DataTable();
            if (Data != null)
            {
                if (!ApplicationContext.Current.IsAdministrator)
                {
                    NewData = Data.Clone();

                    foreach (DataRow Row in Data.Rows)
                    {
                        String Code = Row["NodeCode"].ToString();
                        if (AuthPolicy.HasAuth(Code))
                        {
                            NewData.ImportRow(Row);
                        }
                    }
                }
                else
                {
                    NewData = Data;
                }
            }

            return NewData;
        }

        public string GetProjectCatlogNextCode(string ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select NodeCode from sys_engs_Tree where Scdel=0 and NodeCode like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("[0-9][0-9][0-9][0-9]'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["NodeCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                    Values.Add(Int);
                }
            }

            int i = 1;
            while (Values.Contains(i)) ++i;
            return ParentCode + i.ToString("0000");

        }

        public List<String> GetModuleTables(String TreeCode)
        {
            List<String> Sheets = new List<string>();
            List<String> Result = new List<string>();

            //工程结构树
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("select Sheets from sys_biz_Module where Scdel=0 and ID in (");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select RalationID from sys_engs_Tree where Scdel=0 and NodeCode like '");
            Sql_Select.Append(TreeCode);
            Sql_Select.Append("%' and NodeType = '@module')");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow Row in Data.Rows)
            {
                String sheetList = Row["Sheets"].ToString();
                String[] sheets = sheetList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String sheetname in sheets)
                {
                    if (!Sheets.Contains(sheetname))
                    {
                        Sheets.Add(sheetname);
                    }
                }
            }

            Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            Sql_Select.Append("Select DataTable From sys_biz_Sheet Where Scdel=0 and ID in ('");
            Sql_Select.Append(string.Join("','", Sheets.ToArray()));
            Sql_Select.Append("')");

            Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow Row in Data.Rows)
            {
                String TableName = Row["DataTable"].ToString();
                if (!string.IsNullOrEmpty(TableName))
                {
                    Result.Add(TableName);
                }
            }

            return Result;
        }

        public List<String> GetProjectModuleIndex(String TreeCode)
        {
            List<String> ModuleIDs = new List<string>();

            //工程结构树
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select RalationID from sys_engs_Tree where Scdel=0 and NodeCode like '");
            Sql_Select.Append(TreeCode);
            Sql_Select.Append("%' and NodeType = '@module'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow Row in Data.Rows)
            {
                String ModuleID = Row["RalationID"].ToString();
                if (!string.IsNullOrEmpty(ModuleID))
                {
                    if (!ModuleIDs.Contains(ModuleID))
                    {
                        ModuleIDs.Add(ModuleID);
                    }
                }
            }

            return ModuleIDs;
        }

        public String GetProjectTestModelCode(String TestRoomCode, String ModelID)
        {
            String ModelCode = string.Empty;

            //工程结构树
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select NodeCode from sys_engs_Tree where Scdel=0 and NodeCode like '");
            Sql_Select.Append(TestRoomCode);
            Sql_Select.Append("____' and RalationID = '");
            Sql_Select.Append(ModelID);
            Sql_Select.Append("' and NodeType = '@module'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                ModelCode = Data.Rows[0]["NodeCode"].ToString();
            }

            return ModelCode;
        }
    }
}
