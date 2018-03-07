using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Permissions.Common;
using System.Collections;
using Yqun.Common.ContextCache;

namespace Yqun.BO.PermissionManager
{
    public class RuntimeManager : BOBase
    {
        public bool GetShowTableField(string TableName, string FieldName)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsSystemUser)
                return true;

            StringBuilder sql = new StringBuilder();
            sql.Append("select description,editable,viewable from sys_auth_FieldPermission where ");
            //增加查询条件  Scdel=0  2013-10-17
            sql.Append(" FieldsID in (select ID from sys_auth_Permissions where Scdel=0 and Description = '");
            sql.Append(TableName);
            sql.Append("') and description = '");
            sql.Append(FieldName);
            sql.Append("' ");
            Boolean Result = false;

            DataTable data = GetDataTable(sql.ToString());
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    object r = row["viewable"];

                    if (Convert.ToInt32(r) == 1)
                    {
                        Result = Result | (Convert.ToInt32(r) == 1);
                    }
                    else
                    {
                        Result = Result | (Convert.ToInt32(r) == 1);
                    }

                    return Result;
                }
            }

            return Result;
        }

        public Hashtable GetProjectInformation(String UserName)
        {
            Hashtable ht = new Hashtable();
            ht.Add(NodeType.工程, ObjectInfo.Empty);
            ht.Add(NodeType.标段, ObjectInfo.Empty);
            ht.Add(NodeType.单位, ObjectInfo.Empty);
            ht.Add(NodeType.试验室, ObjectInfo.Empty);

            String UserCode = ApplicationContext.Current.UserCode;
            if (UserCode != "-1" && UserCode.Length >= 4)
            {
                UserCode = UserCode.Substring(0, UserCode.Length - 4);

                try
                {
                    List<string> NodeCodes = new List<string>();
                    if (UserCode.Length >= 4)
                    {
                        NodeCodes.Add(UserCode.Substring(0, 4));
                    }
                    if (UserCode.Length >= 8)
                    {
                        NodeCodes.Add(UserCode.Substring(0, 8));
                    }
                    if (UserCode.Length >= 12)
                    {
                        NodeCodes.Add(UserCode.Substring(0, 12));
                    }
                    if (UserCode.Length >= 16)
                    {
                        NodeCodes.Add(UserCode.Substring(0, 16));
                    }

                    StringBuilder Sql_Select = new StringBuilder();
                    Sql_Select.Append("select ID,NodeCode,NodeType,RalationID,");
                    Sql_Select.Append("(");
                    Sql_Select.Append(" select Description from ");
                    Sql_Select.Append(" 	(");
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
                    Sql_Select.Append("where a.Scdel=0 and a.NodeType != '@module' and a.NodeCode in ('");
                    Sql_Select.Append(string.Join("','", NodeCodes.ToArray()));
                    Sql_Select.Append("') order by NodeCode");

                    DataTable Data = GetDataTable(Sql_Select.ToString());
                    if (Data != null)
                    {
                        foreach (DataRow Row in Data.Rows)
                        {
                            string ID = Row["RalationID"].ToString();
                            string Code = Row["NodeCode"].ToString();
                            string Name = Row["RalationText"].ToString();
                            string Tag = Row["NodeType"].ToString();

                            ObjectInfo Info = new ObjectInfo();
                            Info.Index = ID;
                            Info.Code = Code;
                            Info.Type = Tag.ToLower();
                            Info.Description = Name;

                            switch (Tag.ToLower())
                            {
                                case "@eng":
                                    ht[NodeType.工程] = Info;
                                    break;
                                case "@tenders":
                                    ht[NodeType.标段] = Info;
                                    break;
                                case "@unit_施工单位":
                                case "@unit_监理单位":
                                    ht[NodeType.单位] = Info;
                                    break;
                                case "@folder":
                                    ht[NodeType.试验室] = Info;
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                StringBuilder Sql_Select = new StringBuilder();
                Sql_Select.Append("select ID,Description from sys_engs_ProjectInfo WHERE scdel=0 ");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    string ID = Data.Rows[0]["ID"].ToString();
                    string Code = "0001";
                    string Name = Data.Rows[0]["Description"].ToString();
                    string Tag = "@eng";

                    ObjectInfo Info = new ObjectInfo();
                    Info.Index = ID;
                    Info.Code = Code;
                    Info.Type = Tag.ToLower();
                    Info.Description = Name;

                    ht[NodeType.工程] = Info;
                }
            }

            return ht;
        }
    }
}
