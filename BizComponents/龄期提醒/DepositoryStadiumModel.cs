using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using System.Data;

namespace BizComponents
{
    public class DepositoryStadiumModel
    {
        public static DataTable GetStadiumModels()
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetStadiumModels", new object[] { }) as DataTable;
        }

        public static Boolean UpdateStadiumModels(DataTable Models)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateStadiumModels", new object[] { Models }));
        }
    }
}
