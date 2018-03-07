using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using System.Data;
using Yqun.Services;
using FarPoint.Win;
using System.ComponentModel;
using BizCommon;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;
using System.Collections;

namespace BizComponents
{
    public class DepositoryModuleConfiguration
    {
        public static String GetModuleRelationData(String id)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleRelationData", new object[] { id }).ToString();
        }

        public static String ImportModule(ModuleXMLEntity module)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "ImportModule", new object[] { module }).ToString();
        }

        public static List<IndexDescriptionPair> InitModuleInfo(String Index)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitModuleInfo", new object[] { Index }) as List<IndexDescriptionPair>;
        }

        public static Hashtable InitModelTableInfo(String Index)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitModelTableInfo", new object[] { Index }) as Hashtable;
        }

        public static ModuleConfiguration InitModuleConfiguration(String Index)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitModuleConfiguration", new object[] { Index }) as ModuleConfiguration;
        }

        public static Boolean HaveModuleConfiguration(String Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveModuleConfiguration", new object[] { Index }));
        }
        public static Boolean SettingColumn(String Index)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "AlterNewColumn", new object[] { Index }));
        }

        public static Boolean New(ModuleConfiguration module)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewModuleConfiguration", new object[] { module }));
            return Result;
        }

        public static Boolean Delete(ModuleConfiguration module)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteModuleConfiguration", new object[] { module }));
            return Result;
        }

        public static Boolean Update(ModuleConfiguration module)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateModuleConfiguration", new object[] { module }));
            return Result;
        }

        public static Boolean UpdateSheetConfigurations(ModuleConfiguration module)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateSheetConfigurations", new object[] { module }));
            return Result;
        }

        public static Boolean RemoveSheetConfiguration(ModuleConfiguration module, SheetConfiguration sheet)
        {
            module.Sheets.Remove(sheet);
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateSheetConfiguration", new object[] { module }));
            return Result;
        }

        public static Boolean IsReferenceModel(ModuleConfiguration module)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsReferenceModel", new object[] { module }));
            return Result;
        }
    }

    public class CacheModuleConfiguration
    {
        public static Boolean HaveModuleConfiguration(string Index)
        {
            return DepositoryModuleConfiguration.HaveModuleConfiguration(Index);
        }

        public static ModuleConfiguration InitModuleConfiguration(string Index)
        {
            ModuleConfiguration Model = Agent.CallLocalService("Yqun.BO.BusinessManager.dll", "InitModuleConfiguration", new object[] { Index }) as ModuleConfiguration;
            Hashtable Infos = DepositoryModuleConfiguration.InitModelTableInfo(Index);

            foreach (SheetConfiguration sheet in Model.Sheets)
                sheet.DataTableSchema.Schema = Infos[sheet.Index] as TableDefineInfo;
         
            return Model;
        }

        public static Boolean UpdateCache(String ModelIndex)
        {
            Boolean Result = false;

            List<string> CacheTableNames = new List<string>();
            CacheTableNames.Add("sys_columns");

            List<IndexDescriptionPair> SheetList = DepositoryModuleConfiguration.InitModuleInfo(ModelIndex);
            List<string> DataTables = new List<string>();
            foreach (IndexDescriptionPair pair in SheetList)
                DataTables.Add(pair.DataTable);

            List<string> Wheres = new List<string>();
            if (DataTables.Count > 0)
                Wheres.Add("where TableName in ('" + string.Join("','", DataTables.ToArray()) + "')");
            else
                Wheres.Add("where 1<> 1");

            foreach (string TableName in CacheTableNames)
            {
                int index = CacheTableNames.IndexOf(TableName);
                string where = Wheres[index];
                List<String> IDList = GetNewTableIDList(TableName, where);
                List<String> ExistingIDList = GetExistingTableIDList(TableName, where);
                Result = ExecuteDeletedCommand(TableName, IDList, ExistingIDList);
                Result = Result & ExecuteUpdateCommand(TableName, IDList, ExistingIDList);
            }

            return Result;
        }

        private static List<String> GetNewTableIDList(String TableName, String Where)
        {
            return Agent.CallService("Yqun.BO.LoginBO.dll", "GetNewTableIDList", new object[] { TableName, Where }) as List<String>;
        }

        private static List<String> GetExistingTableIDList(String TableName, String Where)
        {
            return Agent.CallLocalService("Yqun.BO.LoginBO.dll", "GetNewTableIDList", new object[] { TableName, Where }) as List<String>;
        }

        private static Boolean ExecuteDeletedCommand(String TableName, List<string> IDList, List<string> ExistingIDList)
        {
            List<string> DeletedIDList = new List<string>();
            foreach (String id in ExistingIDList)
            {
                if (!IDList.Contains(id))
                    DeletedIDList.Add(id);
            }

            if (DeletedIDList.Count > 0)
            {
                StringBuilder sql_Delete = new StringBuilder();
                sql_Delete.Append("Delete from ");
                sql_Delete.Append(TableName);
                sql_Delete.Append(" Where ID in ('");
                sql_Delete.Append(string.Join("','", DeletedIDList.ToArray()));
                sql_Delete.Append("')");

                object r = Agent.CallLocalService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { sql_Delete.ToString() });
                return (System.Convert.ToInt32(r) == 1);
            }

            return true;
        }

        private static bool ExecuteUpdateCommand(string TableName, List<string> IDList, List<string> ExistingIDList)
        {
            List<string> UpdatedIDList = new List<string>();
            foreach (String id in IDList)
            {
                if (!ExistingIDList.Contains(id))
                    UpdatedIDList.Add(id);
            }

            Boolean Result = false;
            int Count = 1500;
            List<string> group = new List<string>();
            for (int i = 0; i < UpdatedIDList.Count; i++)
            {
                group.Add(UpdatedIDList[i]);

                if (group.Count % Count == 0 && group.Count != 0)
                {
                    DataTable Data = GetNewTableData3(TableName, group);
                    if (group.Count > Count)
                    {
                        Result = Result && UpdateTableData(TableName, Data);
                    }
                    else
                    {
                        Result = UpdateTableData(TableName, Data);
                    }

                    group.Clear();
                }
            }

            if (group.Count > 0)
            {
                DataTable Data = GetNewTableData3(TableName, group);
                if (group.Count > Count)
                {
                    Result = Result && UpdateTableData(TableName, Data);
                }
                else
                {
                    Result = UpdateTableData(TableName, Data);
                }
            }

            return true;
        }

        private static Boolean UpdateTableData(String TableName, DataTable Data)
        {
            if (Data.Rows.Count == 0)
                return true;

            return System.Convert.ToBoolean(Agent.CallLocalService("Yqun.BO.LoginBO.dll", "UpdateTableData", new object[] { TableName, Data }));
        }

        private static DataTable GetNewTableData3(String TableName, List<string> IDList)
        {
            return Agent.CallService("Yqun.BO.LoginBO.dll", "GetNewTableData3", new object[] { TableName, IDList }) as DataTable;
        }
    }
}
