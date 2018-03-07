using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class DepositoryEvaluateCondition
    {
        public static ReportEvaluateCondition InitEvaluateConditions(String ModelIndex, String SheetIndex)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "InitEvaluateConditions", new object[] { ModelIndex, SheetIndex }) as ReportEvaluateCondition;
        }

        public static Boolean UpdateEvaluateCondition(ReportEvaluateCondition EvaluateCondition)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateEvaluateCondition", new object[] { EvaluateCondition }));
        }

        public static Boolean DeleteEvaluateCondition(String ModelIndex, String SheetIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "DeleteEvaluateCondition", new object[] { ModelIndex, SheetIndex }));
        }

        public static Boolean UpdateEvaluateItemCondition(String ModelIndex, String SheetIndex, ItemCondition Item, int OrderIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "UpdateEvaluateItemCondition", new object[] { ModelIndex, SheetIndex, Item, OrderIndex }));
        }

        public static Boolean DeleteEvaluateItemCondition(String ItemIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "DeleteEvaluateItemCondition", new object[] { ItemIndex }));
        }
    }
}
