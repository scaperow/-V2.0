using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Permissions.Common;
using System.Data;
using System.Reflection;

namespace Yqun.BO.PermissionManager
{
    public class UserManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        RoleManager roleManager = new RoleManager();

        public List<User> InitUsers()
        {
            List<User> Users = new List<User>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select ID,Code,UserName,Password,Roles,IsSys from sys_auth_Users where Scdel=0 and Code like '____%' order by UserName");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow row in Data.Rows)
            {
                User user = new User();
                user.Index = row["ID"].ToString();
                user.Name = row["UserName"].ToString();
                user.Code = row["Code"].ToString();
                user.Password = row["Password"].ToString();
                user.Roles = roleManager.InitRoleInformation(user.Index);
                user.IsSys = Convert.ToBoolean(row["IsSys"]);

                Users.Add(user);
            }

            return Users;
        }

        public User InitUsers(String UserIndex)
        {
            User user = null;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select ID,Code,UserName,Password,IsSys from sys_auth_Users where Scdel=0 and ID='");
            Sql_Select.Append(UserIndex);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow row = Data.Rows[0];

                user = new User();
                user.Index = row["ID"].ToString();
                user.Name = row["UserName"].ToString();
                user.Code = row["Code"].ToString();
                user.Password = row["Password"].ToString();
                user.IsSys = Convert.ToBoolean(row["IsSys"]);
            }

            return user;
        }

        public List<User> InitUsers(List<string> UserIndex)
        {
            List<User> users = new List<User>();

            if (string.IsNullOrEmpty(string.Join(",",UserIndex.ToArray())))
                return users;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select ID,Code,UserName,Password,IsSys from sys_auth_Users where Scdel=0 and ID in ('");
            Sql_Select.Append(string.Join("','",UserIndex.ToArray()));
            Sql_Select.Append("')");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    User user = new User();
                    user.Index = Row["ID"].ToString();
                    user.Name = Row["UserName"].ToString();
                    user.Code = Row["Code"].ToString();
                    user.Password = Row["Password"].ToString();
                    user.IsSys = Convert.ToBoolean(Row["IsSys"]);

                    users.Add(user);
                }
            }

            return users;
        }

        public List<User> InitUsers(String[] RoleIndex)
        {
            List<User> users = new List<User>();

            if (RoleIndex == null && RoleIndex.Length == 0)
                return users;

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,Code,UserName,Password,IsSys from sys_auth_Users ");
            if (RoleIndex.Length > 0)
            {
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append(" where Scdel=0 and ");
                List<String> wheres = new List<string>();
                foreach (String role in RoleIndex)
                {
                    wheres.Add("roles like '%," + role + ",%'");
                }
                Sql_Select.Append(string.Join(" or ", wheres.ToArray()));
            }

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    User user = new User();
                    user.Index = Row["ID"].ToString();
                    user.Name = Row["UserName"].ToString();
                    user.Code = Row["Code"].ToString();
                    user.Password = Row["Password"].ToString();
                    user.IsSys = Convert.ToBoolean(Row["IsSys"]);

                    users.Add(user);
                }
            }

            return users;
        }

        public User InitUser(String UserName)
        {
            User user = null;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select ID,Code,UserName,Password,IsSys from sys_auth_Users where Scdel=0 and UserName='");
            Sql_Select.Append(UserName);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow row = Data.Rows[0];

                user = new User();
                user.Index = row["ID"].ToString();
                user.Name = row["UserName"].ToString();
                user.Code = row["Code"].ToString();
                user.Password = row["Password"].ToString();
                user.IsSys = Convert.ToBoolean(row["IsSys"]);
            }

            return user;
        }

        public List<User> InitUser(String[] UserNames)
        {
            List<User> users = new List<User>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select ID,Code,UserName,Password,IsSys from sys_auth_Users where Scdel=0 and UserName in ('");
            Sql_Select.Append(string.Join("','", UserNames));
            Sql_Select.Append("')");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    User user = new User();
                    user.Index = Row["ID"].ToString();
                    user.Name = Row["UserName"].ToString();
                    user.Code = Row["Code"].ToString();
                    user.Password = Row["Password"].ToString();
                    user.IsSys = Convert.ToBoolean(Row["IsSys"]);

                    users.Add(user);
                }
            }

            return users;
        }

        public String GetNextUserCode(String ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Code from sys_auth_Users where Scdel=0 and Code like '");
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

        public Boolean NewUser(User user)
        {
            Boolean Result = false;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select ID,SCTS,Code,UserName,Password,IsSys from sys_auth_Users where Scdel=0 and ID='");
            Sql_Select.Append(user.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = user.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Code"] = user.Code;
                Row["UserName"] = user.Name;
                Row["Password"] = user.Password;
                Row["IsSys"] = user.IsSys;

                Data.Rows.Add(Row);

                try
                {
                    object r = Update(Data);
                    Result = (System.Convert.ToInt32(r) == 1);
                }
                catch
                { }
            }

            return Result;
        }

        public Boolean DeleteUser(String Index)
        {
            StringBuilder Sql_Delete = new StringBuilder();
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            //Sql_Delete.Append("delete from sys_auth_Users where ID='");
            //Sql_Delete.Append(Index);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_auth_Users Set Scts_1=Getdate(),Scdel=1 where ID='");
            Sql_Delete.Append(Index);
            Sql_Delete.Append("'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (System.Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean UpdateUser(User user)
        {
            Boolean Result = false;

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,SCTS,Code,UserName,Password,Roles,IsSys,Scts_1 from sys_auth_Users where ID='");
            Sql_Select.Append(user.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = user.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["Code"] = user.Code;
                Row["UserName"] = user.Name;
                Row["Password"] = user.Password;
                Row["IsSys"] = user.IsSys;
                // 增加字段Scts_1  用于记录信息修改时间     2013-10-17
                Row["Scts_1"] = DateTime.Now.ToString();

                List<string> RoleIndex = new List<string>();
                foreach (Role role in user.Roles)
                {
                    RoleIndex.Add(role.Index);
                }

                if (RoleIndex.Count > 0)
                    Row["Roles"] = string.Concat(",", string.Join(",", RoleIndex.ToArray()), ",");
                else
                    Row["Roles"] = "";

                try
                {
                    object r = Update(Data);
                    Result = (System.Convert.ToInt32(r) == 1);
                }
                catch
                { }
            }

            return Result;
        }

        public Boolean UpdateUser(User[] users)
        {
            if (users == null || users.Length == 0)
                return true;

            Boolean Result = false;

            List<string> strIndexs = new List<string>();
            foreach (User user in users)
                if (user != null)
                    strIndexs.Add(user.Index);

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select ID,SCTS,Code,UserName,Password,Roles,IsSys,Scts_1 from sys_auth_Users where ID in ('");
            Sql_Select.Append(string.Join("','",strIndexs.ToArray()));
            Sql_Select.Append("')");

            //logger.Error(Sql_Select.ToString());

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (User user in users)
                {
                    DataRow DataRow;
                    DataRow[] DataRows = Data.Select("ID='" + user.Index + "'");
                    if (DataRows.Length != 0)
                        DataRow = DataRows[0];
                    else
                    {
                        DataRow = Data.NewRow();
                        Data.Rows.Add(DataRow);
                    }

                    DataRow["ID"] = user.Index;
                    DataRow["SCTS"] = DateTime.Now.ToString();
                    DataRow["Code"] = user.Code;
                    DataRow["UserName"] = user.Name;
                    DataRow["Password"] = user.Password;
                    DataRow["IsSys"] = user.IsSys;
                    // 增加字段Scts_1  用于记录信息修改时间     2013-10-17
                    DataRow["Scts_1"] = DateTime.Now.ToString();

                    List<string> RoleIndex = new List<string>();
                    foreach (Role role in user.Roles)
                    {
                        RoleIndex.Add(role.Index);
                    }

                    if (RoleIndex.Count > 0)
                        DataRow["Roles"] = string.Concat(",", string.Join(",", RoleIndex.ToArray()), ",");
                    else
                        DataRow["Roles"] = "";
                }

                try
                {
                    object r = Update(Data);
                    Result = (System.Convert.ToInt32(r) == 1);
                }
                catch
                { }
            }

            return Result;
        }
    }
}
