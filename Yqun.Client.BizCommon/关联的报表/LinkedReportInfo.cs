using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class LinkedReportInfo
    {
        String _Index;
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        String _NodeFlag;
        public String NodeFlag
        {
            get
            {
                return _NodeFlag;
            }
            set
            {
                _NodeFlag = value;
            }
        }

        String _NodeCode;
        public String NodeCode
        {
            get
            {
                return _NodeCode;
            }
            set
            {
                _NodeCode = value;
            }
        }

        String _ReportID;
        public String ReportID
        {
            get
            {
                return _ReportID;
            }
            set
            {
                _ReportID = value;
            }
        }

        String _ReportName;
        public String ReportName
        {
            get
            {
                return _ReportName;
            }
            set
            {
                _ReportName = value;
            }
        }
    }
}
