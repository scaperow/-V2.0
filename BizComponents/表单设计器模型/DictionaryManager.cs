using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using Yqun.Services;
using System.Data;
using Yqun.Bases;

namespace BizComponents
{
    public class DictionaryManager
    {
        public static DataTable GetAllDictionary()
        {
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件  2013-10-15
            Sql_Select.Append("Select * from sys_Dictionary Where (Scdel IS NULL or Scdel=0) order by CANANYLEVEL, CodeClass, Description");
            
            return Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
        }

        public static DataTable GetDictionaryByCodeClass(String codeClass)
        {
            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件  2013-10-15
            Sql_Select.Append("Select * from sys_Dictionary Where (Scdel IS NULL or Scdel=0) and Code is not null and CodeClass='" + 
                codeClass + "' order by Description");

            return Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
        }

        public static string GetNextCode(String ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            if (ParentCode == "")
            {
                // 增加查询条件  2013-10-15
                //Sql_Select.Append("Select distinct CodeClass from sys_Dictionary where CodeClass like '");
                Sql_Select.Append("Select distinct CodeClass from sys_Dictionary where (Scdel IS NULL or Scdel=0) And CodeClass like '");
                Sql_Select.Append(ParentCode);
                Sql_Select.Append("%'");

                DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
                if (Data != null && Data.Rows.Count > 0)
                {
                    foreach (DataRow row in Data.Rows)
                    {
                        string Value = row["CodeClass"].ToString();
                        int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                        Values.Add(Int);
                    }
                }
            }
            else
            {
                // 增加查询条件  2013-10-15
                //Sql_Select.Append("Select code from sys_Dictionary where Code like '");
                Sql_Select.Append("Select code from sys_Dictionary Where (Scdel IS NULL or Scdel=0) And Code like '");
                Sql_Select.Append(ParentCode);
                Sql_Select.Append("%'");

                DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
                if (Data != null && Data.Rows.Count > 0)
                {
                    foreach (DataRow row in Data.Rows)
                    {
                        string Value = row["Code"].ToString();
                        int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                        Values.Add(Int);
                    }
                }
            }


            int i = 1;
            while (Values.Contains(i)) ++i;

            return ParentCode + i.ToString("00000000");
        }

        public static Boolean SaveDictionary(TreeNode Node)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            //增加查询条件 (Scdel IS NULL or Scdel=0) 2013-10-15
            //sql_select.Append("select * from sys_Dictionary order by CodeClass,Code");
            sql_select.Append("select * from sys_Dictionary Where (Scdel IS NULL or Scdel=0) order by CodeClass,Code");
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
            try
            {
                if (Data != null)
                {
                    foreach (TreeNode node in Node.Nodes)
                    {
                        Selection Sele = node.Tag as Selection;

                        DataRow Row = null;
                        DataRow[] Rows = Data.Select("ID ='" + Sele.ID + "'");
                        if (Rows.Length > 0)
                            Row = Rows[0];
                        else
                        {
                            Row = Data.NewRow();
                            Data.Rows.Add(Row);
                        }

                        Row["ID"] = Sele.ID;
                        Row["Scts"] = DateTime.Now;
                        Row["CodeClass"] = node.Name;
                        Row["DESCRIPTION"] = node.Text;
                        Row["Code"] = null;
                        Row["CANANYLEVEL"] = 0;
                        
                        foreach (TreeNode ItemNode in node.Nodes)
                        {
                            Selection ItemSele = ItemNode.Tag as Selection;

                            DataRow ItemRow = null;
                            DataRow[] ItemRows = Data.Select("ID ='" + ItemSele.ID + "'");
                            if (ItemRows.Length > 0)
                                ItemRow = ItemRows[0];
                            else
                            {
                                ItemRow = Data.NewRow();
                                Data.Rows.Add(ItemRow);
                            }

                            ItemRow["ID"] = ItemSele.ID;
                            ItemRow["Scts"] = DateTime.Now;
                            ItemRow["CodeClass"] = node.Name;
                            ItemRow["DESCRIPTION"] = ItemNode.Text;
                            ItemRow["Code"] = ItemNode.Name;
                            ItemRow["CANANYLEVEL"] = 1;
                        }
                    }
                }

                object r = Agent.CallService("Yqun.BO.LoginBO.dll", "Update", new object[] { Data });
                Result = (System.Convert.ToInt32(r) == 1);

                return Result;

            }
            catch
            {
            }

            return Result;
        }

        public static Boolean ModifyDictionary(TreeNode Node, String Rename)
        {
            Boolean Result = false;

            Selection sele = Node.Tag as Selection;
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 (Scdel IS NULL or Scdel=0) 2013-10-15
            Sql_Select.Append("Select * from sys_Dictionary where (Scdel IS NULL or Scdel=0) and CodeClass = '");
            Sql_Select.Append(Node.Name);
            Sql_Select.Append("' and length = '");
            Sql_Select.Append(sele.Value.Length);
            Sql_Select.Append("'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                Data.Rows[0]["description"] = Rename;
                //更新Scts_1 为当前时间   2013-10-15
                Data.Rows[0]["Scts_1"] = DateTime.Now.ToString();
            }

            object r = Agent.CallService("Yqun.BO.LoginBO.dll", "Update", new object[] { Data });
            Result = (System.Convert.ToInt32(r) == 1);

            return Result;
        }

        public static Boolean DeleteDictionary(TreeNode Node)
        {
            Boolean Result = false;

            Selection sele = Node.Tag as Selection;
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("delete from sys_Dictionary where id = '");
            //Sql_Delete.Append(sele.ID);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_Dictionary Set Scts_1='" + DateTime.Now + "',Scdel=1");
            Sql_Delete.Append(" Where id='");
            Sql_Delete.Append(sele.ID);
            Sql_Delete.Append("'");

            object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Sql_Delete.ToString() });
            Result = (Convert.ToInt32(r) == 1);
            return Result;
        }

        public static Boolean ModifyDictionaryItem(TreeNode Node, String Rename)
        {
            Boolean Result = false;

            Selection sele = Node.Tag as Selection;
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select * from sys_Dictionary where Scdel=0 and Code = '");
            Sql_Select.Append(Node.Name);
            Sql_Select.Append("' and codeclass = '");
            Sql_Select.Append(sele.Value);
            Sql_Select.Append("'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                Data.Rows[0]["description"] = Rename;
                //更新Scts_1 为当前时间   2013-10-15
                Data.Rows[0]["Scts_1"] = DateTime.Now.ToString();
            }

            object r = Agent.CallService("Yqun.BO.LoginBO.dll", "Update", new object[] { Data });
            Result = (System.Convert.ToInt32(r) == 1);
            return Result;
        }
    }
}
