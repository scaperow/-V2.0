using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using BizCommon;
using System.Data;

namespace BizComponents
{
    public class DeviceHelperClient
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool? AddDevice(Sys_Device device)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "AddDevice", new object[] { device }) as bool?;
        }

        public static bool? EditDevice(Sys_Device device)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "EditDevice", new object[] { device }) as bool?;
        }

        public static bool? DeleteDevice(List<string> IDs)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteDevice", new object[] { IDs }) as bool?;
        }

        public static Sys_Page GetDeviceList(int index, int size, string query)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDeviceList", new object[] { index, size, query }) as Sys_Page;
        }

        public static string GenerateMachineCode(string testRoomCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GenerateMachineCode", new object[] { testRoomCode }) as string;
        }

        public static DataTable GetDevice(string machineCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDevice", new object[] { machineCode }) as DataTable;
        }

        public static DataSet GetDeviceSummary(string testRoomCode, int meet, int pageIndex, int pageSize)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDeviceSummary", new object[] {
                    testRoomCode,  meet,  pageIndex,  pageSize}) as DataSet;
        }
    }
}
