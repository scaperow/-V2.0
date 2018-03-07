using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon.Chart;

namespace ReportCommon
{
    [Serializable]
    public class ChartAxisAtt
    {
        public static ChartAxisAtt Empty = new ChartAxisAtt();

        Object _DataDefinition = new ChartDataDefinition();
        [NonSerialized]
        String _SeriesTitle = string.Empty;
        [NonSerialized]
        DataList _DataList = new DataList();

        public object DataDefinition
        {
            get
            {
                return _DataDefinition;
            }
            set
            {
                _DataDefinition = value;
            }
        }

        public String SeriesTitle
        {
            get
            {
                return _SeriesTitle;
            }
            set
            {
                _SeriesTitle = value;
            }
        }

        public DataList DataList
        {
            get
            {
                return _DataList;
            }
            set
            {
                _DataList = value;
            }
        }
    }
}
