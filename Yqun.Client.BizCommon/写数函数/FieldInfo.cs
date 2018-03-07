using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class FieldInfo
    {
        private String _Name;
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private String _Text;
        public String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

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

        private String _TableText;
        public String TableText
        {
            get
            {
                return _TableText;
            }
            set
            {
                _TableText = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
