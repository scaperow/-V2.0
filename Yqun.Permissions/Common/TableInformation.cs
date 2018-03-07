using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions
{
    public class TableInformation
    {
        private String _TableName;
        public String TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        private String _TableInform;
        public String TableInform
        {
            get
            {
                return _TableInform;
            }
            set
            {
                _TableInform = value;
            }
        }

        public override string ToString()
        {
            return _TableInform;
        }
    }
}
