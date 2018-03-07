using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;
using System.Diagnostics;
using FarPoint.Win;
using FarPoint.Win.Spread;
using BizCommon;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;

namespace BizComponents
{
    public class DepositorySheetConfiguration
    {
        public static List<IndexDescriptionPair> InitDataTableInfo(String[] Index)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitDataTableInfo", new object[] { Index }) as List<IndexDescriptionPair>;
        }

        public static List<IndexDescriptionPair> InitSheetInfo(String[] Index)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitSheetInfo", new object[] { Index }) as List<IndexDescriptionPair>;
        }

        public static List<SheetConfiguration> InitConfiguration(IndexDescriptionPair[] Pairs)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitSheetConfiguration", new object[] { Pairs }) as List<SheetConfiguration>;
        }

        public static SheetConfiguration InitConfiguration(string Index)
        {
            List<SheetConfiguration> Configurations = InitConfiguration(new string[] { Index });

            if (Configurations.Count > 0)
                return Configurations[0];

            return null;
        }

        public static List<SheetConfiguration> InitConfiguration(string[] Indexes)
        {
            List<SheetConfiguration> Configurations = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitSheetConfiguration", new object[] { Indexes }) as List<SheetConfiguration>;
            return Configurations;
        }

        public static Boolean HaveConfiguration(string Index)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveSheetConfiguration", new object[] { Index }));
            return Result;
        }

        public static bool New(SheetConfiguration Sheet)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewSheetConfiguration", new object[] { Sheet }));
            return Result;
        }

        public static bool Update(SheetConfiguration Sheet)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateSheetConfiguration", new object[] { Sheet }));
            return Result;
        }

        public static bool Delete(SheetConfiguration Sheet)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteSheetConfiguration", new object[] { Sheet }));
            return Result;
        }

        public static List<String> IsReferenceSheet(SheetConfiguration Sheet)
        {
            List<String> Result = Agent.CallService("Yqun.BO.BusinessManager.dll", "IsReferenceSheet", new object[] { Sheet }) as List<String>;
            return Result;
        }

        public static Boolean CopySheet(String SheetIndex, String NewSheetIndex, String NewSheetCode, String NewSheetName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "CopySheet", new object[] { SheetIndex, NewSheetIndex, NewSheetCode, NewSheetName }));
        }

        public static string GetSheetIndex(string SheetCode)
        {
            string SheetIndex = "";

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-17
            Sql_Select.Append("Select ID from sys_biz_Sheet where Scdel=0 and CatlogCode='");
            Sql_Select.Append(SheetCode);
            Sql_Select.Append("'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                SheetIndex = Data.Rows[0]["ID"].ToString();
            }

            return SheetIndex;
        }

        public static string GetSheetName(string FolderCode, string SheetName)
        {
            string tempSheetName = SheetName;
            Boolean Have = true;
            int Index = 1;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-17
            Sql_Select.Append("Select Description from sys_biz_Sheet where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("[0-9][0-9][0-9][0-9]'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                while (Have)
                {
                    Have = false;
                    foreach (DataRow row in Data.Rows)
                    {
                        if (tempSheetName == row["Description"].ToString())
                        {
                            Have = true;
                            break;
                        }
                    }

                    if (Have)
                    {
                        tempSheetName = SheetName + "_" + (Index++).ToString();
                    }
                }
            }

            return tempSheetName;
        }

        public static string GetFolderName(string FolderCode, string FolderName)
        {
            string tempFolderName = FolderName;
            Boolean Have = true;
            int Index = 1;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-17
            Sql_Select.Append("Select CatlogName from sys_biz_SheetCatlog where Scdel=0 and CatlogCode like '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("[0-9][0-9][0-9][0-9]'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                while (Have)
                {
                    Have = false;
                    foreach (DataRow row in Data.Rows)
                    {
                        if (tempFolderName == row["CatlogName"].ToString())
                        {
                            Have = true;
                            break;
                        }
                    }

                    if (Have)
                    {
                        tempFolderName = FolderName + "_" + (Index++).ToString();
                    }
                }
            }

            return tempFolderName;
        }

    }
}
