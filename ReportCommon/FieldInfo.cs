using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    /// <summary>
    /// 表字段信息
    /// </summary>
    [Serializable]
    public class FieldInfo
    {
        string _Name;
        public String FieldName
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

        string _Description;
        public String FieldDescription
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

        string _DataType;
        public String FieldDataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                _DataType = value;
            }
        }

        public override string ToString()
        {
            return FieldDescription;
        }
    }
}
