using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    [Serializable]
    public class ReportDataDefinition
    {
        String _CatlogArea;
        public String CatlogArea
        {
            get
            {
                return _CatlogArea;
            }
            set
            {
                _CatlogArea = value;
            }
        }

        String _SeriesArea;
        public String SeriesArea
        {
            get
            {
                return _SeriesArea;
            }
            set
            {
                _SeriesArea = value;
            }
        }

        String _SeriesNameArea;
        public String SeriesNameArea
        {
            get
            {
                return _SeriesNameArea;
            }
            set
            {
                _SeriesNameArea = value;
            }
        }
    }
}
