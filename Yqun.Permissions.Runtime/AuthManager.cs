using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using System.Data;
using Yqun.Services;
using Yqun.Bases;
using Yqun.Permissions.Common;

namespace Yqun.Permissions.Runtime
{
    /// <summary>
    /// 权限管理者
    /// </summary>
    public class AuthManager 
    {
        #region 表单数据项的可见性

        public static bool GetShowTableField(string TableName, string FieldName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.PermissionManager.dll", "GetShowTableField", new object[] { TableName, FieldName }));
        }

        #endregion 表单数据项的可见性

        #region 模块权限

        public static bool GetModuleFolder(string FolderCode, string OfSolution)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return true;

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select name From SYS_SOLCONTENT Where OFFOLDER like '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("%'and OFSOLUTION='");
            Sql_Select.Append(OfSolution);
            Sql_Select.Append("' order by orderindex");

            Boolean result = false;
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ID = Row["name"].ToString();
                    Boolean r = GetModuleAuth(ID);
                    result = result | r;
                }
            }

            return result;
        }

        public static bool GetModuleAuth(string BizID)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return true;

            Boolean Result = false;
            foreach (Role role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    FunctionsPermission functionsPermission = permission as FunctionsPermission;
                    if (functionsPermission != null && functionsPermission.ModuleID == BizID)
                    {
                        Result = (functionsPermission.Functions.Count > 0);
                        break;
                    }
                    if (permission.ModuleID == BizID)
                    {
                        Result = true;
                        break;
                    }
                }
            }

            return Result;
        }

        #endregion 模块权限

        #region 表单权限

        public static Dictionary<string, Boolean> Sheets = new Dictionary<string, bool>();
        public static Boolean GetExternalSheetAuth(string SheetName)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return true;

            Boolean Result = false;
            foreach (Role role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    FieldsPermission fieldspermission = permission as FieldsPermission;
                    if (fieldspermission != null && fieldspermission.FieldsName.ToLower() == SheetName.ToLower())
                    {

                        foreach (FieldPermission fieldpermission in fieldspermission.Fields)
                        {
                            Result = Result | fieldpermission.Viewable | fieldpermission.Editable;
                            if (Result)
                            {
                                break;
                            }
                        }

                        for (int i = 0; i < Sheets.Count; i++)
                        {
                            if (Sheets.ContainsKey(SheetName))
                            {
                                Sheets.Add(SheetName, Result);
                            }
                        }
                    }
                }
            }

            return Result;
        }

        #endregion 表单权限

        #region 列权限

        /// <summary>
        /// 查找列权限
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static FieldPermission GetFieldsPermission(string TableName, string FieldName)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return null;

            FieldPermission Result = null;
            foreach (Role role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    FieldsPermission fieldspermission = permission as FieldsPermission;
                    if (fieldspermission != null && fieldspermission.FieldsName.ToLower() == TableName.ToLower())
                    {
                        foreach (FieldPermission fieldpermission in fieldspermission.Fields)
                        {
                            if (fieldpermission.FieldName.ToLower() == FieldName.ToLower())
                            {
                                Result = fieldpermission;
                                break;
                            }
                        }
                    }
                }
            }

            return Result;
        }

        #endregion 列权限

        #region 功能权限

        public static Boolean GetFunctionAuth(string BizID, string Message)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return true;

            Boolean Result = false;
            foreach (Role role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    if (permission is FunctionsPermission && permission.ModuleID == BizID)
                    {
                        FunctionsPermission Functions = permission as FunctionsPermission;
                        foreach (FunctionPermission Function in Functions.Functions)
                        {
                            if (string.Compare(Function.Index, Message, true) == 0)
                            {
                                Result = true;
                                break;
                            }
                        }
                    }
                }
            }

            return Result;
        }

        #endregion 功能权限

        #region 数据权限

        /// <summary>
        /// 数据权限
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        public static String GetDataCondition(String TableName, String Prefix)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return "";

            String Result = string.Empty;

            foreach (Role role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    if (permission is DatasPermission && permission.Caption == TableName)
                    {
                        List<string> list = new List<string>();
                        DatasPermission datapermis = permission as DatasPermission;
                        foreach (DataPermission dataper in datapermis.Conditions)
                        {
                            string InStr;
                            InStr = string.Join("','", dataper.Values.ToArray());
                            list.Add(dataper.FieldName + " not in (" + InStr + ")");
                        }

                        Result = string.Join(" and ", list.ToArray());

                        Result = " " + Prefix + " " + Result;
                        Result = Result.Replace("\"", "'");

                        return Result;
                    }
                }
            }

            return Result;
        }

        #endregion 数据权限

        #region 行权限

        public static IAuthPolicy GetTreeAuth(String TreeID)
        {
            List<RecordListElement> RecordListElements = new List<RecordListElement>();
            foreach (Role role in Yqun.Common.ContextCache.ApplicationContext.Current.Roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    if (permission is RecordsPermission)
                    {
                        RecordsPermission records = permission as RecordsPermission;
                        if (records.Caption == TreeID)
                        {
                            foreach (RecordListElement Element in records.RecordPermissionList)
                            {
                                if (!RecordListElements.Contains(Element))
                                {
                                    RecordListElements.Add(Element);
                                }
                            }
                        }
                    }
                }
            }

            return new AuthPolicy(RecordListElements);
        }

        public static IAuthPolicy GetTreeAuth(String TreeID, PermissionCollection Permissions)
        {
            List<RecordListElement> RecordListElements = new List<RecordListElement>();

            foreach (Permission permission in Permissions)
            {
                if (permission is RecordsPermission)
                {
                    RecordsPermission records = permission as RecordsPermission;
                    if (records.Caption == TreeID)
                    {
                        foreach (RecordListElement Element in records.RecordPermissionList)
                        {
                            if (!RecordListElements.Contains(Element))
                            {
                                RecordListElements.Add(Element);
                            }
                        }
                    }
                }
            }

            return new AuthPolicy(RecordListElements);
        }

        #endregion 行权限
    }

    /// <summary>
    /// 行权限策略
    /// </summary>
    public class AuthPolicy : IAuthPolicy
    {
        List<RecordListElement> m_RecordListElements = new List<RecordListElement>();
        public AuthPolicy(List<RecordListElement> RecordListElements)
        {
            m_RecordListElements = RecordListElements;
        }

        #region IAuthPolicy 成员

        public bool HasAuth(string Code)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                return true;

            bool Result = false;
            if (m_RecordListElements == null)
                return Result;

            foreach (RecordListElement Element in m_RecordListElements)
            {
                if (Code.StartsWith(Element.Code) || Element.Code.StartsWith(Code))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        #endregion
    }
}
