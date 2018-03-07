using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    [Serializable]
    public class ChartDataDefinition
    {
        List<string> _CatlogAxises = new List<string>();
        public List<string> CatlogAxises
        {
            get
            {
                return _CatlogAxises;
            }
            set
            {
                _CatlogAxises = value;
            }
        }

        Boolean _DistinctCatlog;
        public Boolean DistinctCatlog
        {
            get
            {
                return _DistinctCatlog;
            }
            set
            {
                _DistinctCatlog = value;
            }
        }

        String _SeriesAxis;
        public String SeriesAxis
        {
            get
            {
                return _SeriesAxis;
            }
            set
            {
                _SeriesAxis = value;
            }
        }

        FunctionInfo _FunctionInfo = FunctionInfo.None;
        public FunctionInfo FunctionInfo
        {
            get
            {
                return _FunctionInfo;
            }
            set
            {
                _FunctionInfo = value;
            }
        }
    }
}
