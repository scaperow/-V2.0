using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Permissions.Common;

namespace Yqun.BO.PermissionManager
{
    public class OrganizationManager : BOBase
    {
        public DataTable InitOrganization()
        {
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,NodeCode,NodeType,RalationID,");
            Sql_Select.Append("(");
            Sql_Select.Append(" select Description from ");
            Sql_Select.Append("	(");
            Sql_Select.Append("     select ID,Description from sys_engs_ProjectInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_CompanyInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_SectionInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_ItemInfo");
            Sql_Select.Append("	) as b where b.ID = a.RalationID");
            Sql_Select.Append(") as RalationText,'true' as IsNode ");
            Sql_Select.Append("from sys_engs_Tree as a ");
            //增加查询条件 Scdel=0   2013-10-17
            Sql_Select.Append("where a.Scdel=0 and a.NodeType != '@module' ");
            Sql_Select.Append(" union ");
            //增加查询条件 Scdel=0   2013-10-17
            Sql_Select.Append("select ID,Code as NodeCode,'@user',ID as RalationID,UserName as RalationText,'false' as IsNode from sys_auth_users where Scdel=0 and IsSys = 'false' and UserName !='downloader'");
            Sql_Select.Append("order by isNode desc,NodeCode");

            return GetDataTable(Sql_Select.ToString());
        }

        public DataTable InitOrganizationWithoutUserName()
        {
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,NodeCode,NodeType,RalationID,");
            Sql_Select.Append("(");
            Sql_Select.Append(" select Description from ");
            Sql_Select.Append("	(");
            Sql_Select.Append("     select ID,Description from sys_engs_ProjectInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_CompanyInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_SectionInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_ItemInfo");
            Sql_Select.Append("	) as b where b.ID = a.RalationID");
            Sql_Select.Append(") as RalationText,'true' as IsNode ");
            Sql_Select.Append("from sys_engs_Tree as a ");
            //增加查询条件 Scdel=0   2013-10-17
            Sql_Select.Append("where a.Scdel=0 and a.NodeType != '@module' ");
            Sql_Select.Append("order by IsNode desc,NodeCode");

            return GetDataTable(Sql_Select.ToString());
        }

        public DataTable InitOrganization(String[] UserNames)
        {
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,NodeCode,NodeType,RalationID,");
            Sql_Select.Append("(");
            Sql_Select.Append(" select Description from ");
            Sql_Select.Append("	(");
            Sql_Select.Append("     select ID,Description from sys_engs_ProjectInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_CompanyInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_SectionInfo");
            Sql_Select.Append("     union");
            Sql_Select.Append("     select ID,Description from sys_engs_ItemInfo");
            Sql_Select.Append("	) as b where b.ID = a.RalationID");
            Sql_Select.Append(") as RalationText,'true' as IsNode ");
            Sql_Select.Append("from sys_engs_Tree as a ");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("where Scdel=0 and a.NodeType != '@module' ");
            Sql_Select.Append(" union ");
            //增加查询条件 Scdel=0   2013-10-17
            Sql_Select.Append("select ID,Code as NodeCode,'@user',ID as RalationID,UserName as RalationText,'false' as IsNode from sys_auth_users where Scdel=0 and IsSys = 'false' and UserName in ('");
            Sql_Select.Append(string.Join("','", UserNames));
            Sql_Select.Append("') ");
            Sql_Select.Append("order by IsNode desc,NodeCode");

            return GetDataTable(Sql_Select.ToString());
        }

        public String GetNextOrganizationCode(String ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select Code from sys_auth_Organization where Code like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("____'");

            DataTable Data = GetDataTable(Sql_Select.ToString()) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["Code"].ToString();
                    int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                    Values.Add(Int);
                }
            }

            int i = 1;
            while (Values.Contains(i)) ++i;

            return ParentCode + i.ToString("0000");
        }

        public String GetUserLoginName(String TrueName)
        {
            string UserName = string.Empty;
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select username from sys_auth_Users where Scdel=0 and TrueName = '");
            Sql_Select.Append(TrueName);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString()) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    UserName = row["username"].ToString();
                }
            }


            return UserName;
        }
    }
}
