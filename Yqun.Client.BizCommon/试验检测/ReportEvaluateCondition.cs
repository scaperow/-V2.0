using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ReportEvaluateCondition
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

        String _ModelIndex;
        public String ModelIndex
        {
            get
            {
                return _ModelIndex;
            }
            set
            {
                _ModelIndex = value;
            }
        }

        String _SheetIndex;
        public String SheetIndex
        {
            get
            {
                return _SheetIndex;
            }
            set
            {
                _SheetIndex = value;
            }
        }

        String _Description;
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        String _ReportNumber;
        public String ReportNumber
        {
            get
            {
                return _ReportNumber;
            }
            set
            {
                _ReportNumber = value;
            }
        }

        String _ReportDate;
        public String ReportDate
        {
            get
            {
                return _ReportDate;
            }
            set
            {
                _ReportDate = value;
            }
        }

        List<ItemCondition> _Items = new List<ItemCondition>();
        public List<ItemCondition> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
            }
        }
    }
}
