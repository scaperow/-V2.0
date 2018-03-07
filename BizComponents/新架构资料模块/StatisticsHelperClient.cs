using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Yqun.Services;
using BizCommon;
using Newtonsoft.Json;

namespace BizComponents
{
    public class StatisticsHelperClient
    {
        public const string DLL = "Yqun.BO.BusinessManager.dll";

        public static DataTable GetStatisticsList()
        {
            return Agent.CallService(DLL, "GetStatisticsList", new object[] { }) as DataTable;
        }

        public static string NewStatistics(Sys_Statistics_Item model)
        {
            return Agent.CallService(DLL, "NewStatistics", new object[] { model }) as string;
        }

        public static string ModifyStatistics(Sys_Statistics_Item model)
        {
            return Agent.CallService(DLL, "ModifyStatistics", new object[] { model }) as string;
        }

        public static string DeleteStatistics(Guid[] ids)
        {
            return Agent.CallService(DLL, "DeleteStatistics", new object[] { ids }) as string;
        }

        public static DataTable GetStatistics(Guid id)
        {
            return Agent.CallService(DLL, "GetStatistics", new object[] { id }) as DataTable;
        }

        internal static string GetStatisticsMapOnModule(Guid moduleID)
        {
            return Agent.CallService(DLL, "GetStatisticsMapOnModule", new object[] { moduleID }) as string;
        }

        internal static string UpdateStatisticsMapOnModule(Guid moduleID, string map)
        {
            return Agent.CallService(DLL, "UpdateStatisticsMapOnModule", new object[] { moduleID, map }) as string;
        }

        internal static DataTable GetStatisticsModules(Guid itemID)
        {
            return Agent.CallService(DLL, "GetStatisticsModules", new object[] { itemID }) as DataTable;
        }

        internal static string NewStatisticsReference(Guid statisticsID, Guid moduleID)
        {
            return Agent.CallService(DLL, "NewStatisticsReference", new object[] { statisticsID, moduleID }) as string;
        }

        internal static string SynchronousStatistics(Guid statisticsID, List<StatisticsSetting> items)
        {

            return Agent.CallService(DLL, "SynchronousStatistics", new object[] { JsonConvert.SerializeObject(items) }) as string;
        }

        internal static string RemoveStatisticsReference(Guid statisticsID, Guid moduleID)
        {
            return Agent.CallService(DLL, "RemoveStatisticsReference", new object[] { statisticsID, moduleID }) as string;
        }

        internal static DataTable GetFactories(string columns, string order)
        {
            return Agent.CallService(DLL, "GetFactories", new object[] { columns, order }) as DataTable;
        }

        internal static Sys_Factory GetFactory(string column, Guid id)
        {
            return Agent.CallService(DLL, "GetFactory", new object[] { column, id }) as Sys_Factory;
        }

        internal static string MergeFactory(string[] ids, Sys_Factory factory)
        {
            return Agent.CallService(DLL, "MergeFactory", new object[] { ids, factory }) as string;
        }
    }
}
