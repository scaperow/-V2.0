using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Permissions.Common;
using System.Reflection;

namespace Yqun.BO.PermissionManager
{
    public class RoleManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable InitRoleInformation()
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
            Sql_Select.Append(") as RalationText,'true' as IsNode,'false' as IsAdministrator ");
            Sql_Select.Append("from sys_engs_Tree as a ");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("where Scdel=0 and a.NodeType != '@module' ");
            Sql_Select.Append(" union ");
            Sql_Select.Append("select ID,Code as NodeCode,'@role',ID as RalationID,Name as RalationText,'false' as IsNode,IsAdministrator from sys_auth_Roles ");
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append(" Where Scdel=0 ");
            Sql_Select.Append("order by IsNode desc,NodeCode");

            //logger.Error(Sql_Select.ToString());

            return GetDataTable(Sql_Select.ToString());
        }

        public RoleCollection InitRoleInformation(String UserIndex)
        {
             RoleCollection Result = new RoleCollection();

            if (string.IsNullOrEmpty(UserIndex))
                return Result;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select Roles from sys_auth_Users where Scdel=0 and ID='");
            Sql_Select.Append(UserIndex);
            Sql_Select.Append("'");

            String Roles = ExcuteScalar(Sql_Select.ToString()) as String;
            if (Roles != null)
            {
                String[] Role = Roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Result = InitRoleInformation(Role);
            }

            return Result;
        }

        public RoleCollection InitRoleInformation(String[] RoleIndex)
        {
            RoleCollection Result = new RoleCollection();

            if (RoleIndex == null || RoleIndex.Length == 0)
                return Result;

            if (string.IsNullOrEmpty(string.Join("','", RoleIndex)))
                return Result;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select * from sys_auth_Roles where Scdel=0 and ID in ");
            Sql_Select.Append(string.Concat("('", string.Join("','", RoleIndex), "')"));

            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow row in Data.Rows)
            {
                String Index = row["ID"].ToString();
                String Name = row["Name"].ToString();
                String Code = row["Code"].ToString();
                String IsAdministrator = row["IsAdministrator"].ToString();
                String Permissions = row["Permissions"].ToString();

                Role role = new Role();
                role.Index = Index;
                role.Name = Name;
                role.Code = Code;
                role.IsAdministrator = Convert.ToBoolean(IsAdministrator);

                Result.Add(role);
            }

            return Result;
        }

        public String GetNextRoleCode(String ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Code from sys_auth_Roles where Scdel=0 and Code like '");
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

        public Boolean NewRole(Role role)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select * from sys_auth_Roles where Scdel=0 and ID='");
            Sql_Select.Append(role.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = role.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Code"] = role.Code;
                Row["Name"] = role.Name;
                Row["IsAdministrator"] = role.IsAdministrator;
                Row["Permissions"] = "";
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean DeleteRole(String Index)
        {
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_auth_Roles Where ID='");
            //Sql_Delete.Append(Index);
            //Sql_Delete.Append("'");
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            Sql_Delete.Append("Update sys_auth_Roles Set Scts_1=Getdate(),Scdel=1 Where ID='");
            Sql_Delete.Append(Index);
            Sql_Delete.Append("'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean UpdateRole(Role role)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select * from sys_auth_Roles where Scdel=0 and ID='");
            Sql_Select.Append(role.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = role.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Code"] = role.Code;
                Row["Name"] = role.Name;
                Row["Scts_1"] = DateTime.Now.ToString();

                List<string> Indexs = new List<string>();
                foreach (Permission permission in role.Permissions)
                {
                    Indexs.Add(permission.Index);
                }

                if (Indexs.Count > 0)
                    Row["Permissions"] = string.Concat(",", string.Join(",", Indexs.ToArray()), ",");
                else
                    Row["Permissions"] = "";

                //增加字段 Scts_1 用时记录数据修改时间。        2013-10-17
                Row["Scts_1"] = DateTime.Now.ToString();
            }

            Boolean Result = false;

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
