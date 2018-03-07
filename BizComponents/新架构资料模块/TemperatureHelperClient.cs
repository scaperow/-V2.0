using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class TemperatureHelperClient
    {
        public static DataTable GetTemperatureTypes(string testRoomCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTemperatureTypes", new object[] { testRoomCode }) as DataTable;
        }

        public static bool ExistsTemperatureName(string testRoomCode, string name)
        {
            return (bool)Agent.CallService("Yqun.BO.BusinessManager.dll", "ExistsTemperatureName", new object[] { testRoomCode, name });
        }

        public static string DeleteTemperature(string id)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteTemperature", new object[] { id }) as string;
        }

        public static string RenameTemperature(string id, string newName)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "RenameTemperature", new object[] { id, newName }) as string;
        }

        internal static string NewTemperature(Sys_Temperature temperature)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "NewTemperature", new object[] { temperature }) as string;
        }

        public static DataTable GetDocumentTemperaturesRange(string documentID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentTemperaturesRange", new object[] { documentID }) as DataTable;
        }
    }
}
