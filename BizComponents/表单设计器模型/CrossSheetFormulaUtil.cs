using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Services;

namespace BizComponents
{
    public class CrossSheetFormulaUtil
    {
        public static Boolean HaveCrossSheetFormulaInfoByModel(string ModelIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveCrossSheetFormulaInfoByModel", new object[] { ModelIndex }));
        }

        public static List<CrossSheetFormulaInfo> getCrossSheetFormulaInfoByModel(string ModelIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "getCrossSheetFormulaInfoByModel", new object[] { ModelIndex }) as List<CrossSheetFormulaInfo>;
        }

        public static List<CrossSheetFormulaInfo> getCrossSheetFormulaInfoBySheet(string SheetIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "getCrossSheetFormulaInfoBySheet", new object[] { SheetIndex }) as List<CrossSheetFormulaInfo>;
        }

        public static Boolean saveCrossSheetFormulaInfos(string ModelIndex, List<CrossSheetFormulaInfo> Infos)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "saveCrossSheetFormulaInfos", new object[] { ModelIndex, Infos }));
        }

        public static Boolean saveCrossSheetFormulaInfos(List<CrossSheetFormulaInfo> Infos)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "saveCrossSheetFormulaInfos", new object[] { Infos }));
        }

        public static Boolean removeCrossSheetFormulaInfos(String ModelIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "removeCrossSheetFormulaInfos", new object[] { ModelIndex }));
        }

        public static Boolean removeCrossSheetFormulaInfos(String ModelIndex, String SheetIndex)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "removeCrossSheetFormulaInfos", new object[] { ModelIndex, SheetIndex }));
        }
    }
}
