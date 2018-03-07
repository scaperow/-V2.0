using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class StatisticsModuleSetting
    {
        public string StatisticsItemName { set; get; }
        public Guid SheetID { set; get; }
        public string SheetName { set; get; }
        public string CellName { set; get; }
        public string BindField { set; get; }
    }

    public class StatisticsSetting
    {
        public string ItemName { set; get; }
        public string BindField { set; get; }
    }
}
