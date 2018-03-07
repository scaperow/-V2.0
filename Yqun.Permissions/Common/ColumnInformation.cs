using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;

namespace Yqun.Permissions
{
    public class ColumnInformation
    {
        private String _ColumName;
        private String _ColumInfo;
        private String _ColumType;

        public String ColumName
        {
            get
            {
                return _ColumName;
            }
            set
            {
                _ColumName = value;
            }
        }

        public String ColumInfo
        {
            get
            {
                return _ColumInfo;
            }
            set
            {
                _ColumInfo = value;
            }
        }

        public String ColumType
        {
            get
            {
                return _ColumType;
            }
            set
            {
                _ColumType = value;
            }
        }

        SheetReference _Reference;
        public SheetReference Reference
        {
            get
            {
                return _Reference;
            }
            set
            {
                _Reference = value;
            }
        }

        public override string ToString()
        {
            return _ColumInfo;
        }
    }
}
