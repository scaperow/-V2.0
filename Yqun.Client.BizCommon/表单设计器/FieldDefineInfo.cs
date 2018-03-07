using System;
using System.Data;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;
using System.Runtime.Serialization;

namespace BizCommon
{
    /// <summary>
    /// 数据库表中的字段信息
    /// </summary>
    [Serializable]
    public class FieldDefineInfo
    {
        public FieldDefineInfo(TableDefineInfo TableInfo)
        {
            m_TableInfo = TableInfo;
            if (m_TableInfo != null)
                m_TableInfo.FieldInfos.Add(this);

            m_Index = Guid.NewGuid().ToString();
        }

        TableDefineInfo m_TableInfo;
        public TableDefineInfo TableInfo
        {
            get
            {
                return m_TableInfo;
            }
        }

        private string m_Index;
        public string Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
            }
        }

        /// <summary>
        /// 字段的数据库名字
        /// </summary>
        string m_FieldName;
        public string FieldName
        {
            get
            {
                return m_FieldName;
            }
            set
            {
                m_FieldName = value;
            }
        }

        /// <summary>
        /// 字段的描述
        /// </summary>
        string _Description;
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_Description))
                    _Description = "数据项";

                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        /// <summary>
        /// 字段对应的表单中的区域
        /// </summary>
        /// <remarks>
        /// 在用于表外数据项时该字段没有用
        /// </remarks>
        string _RangeInfo;
        public string RangeInfo
        {
            get
            {
                return _RangeInfo;
            }
            set
            {
                _RangeInfo = value;
            }
        }

        FieldType _FieldType;
        public FieldType FieldType
        {
            get
            {
                return _FieldType;
            }
            set
            {
                _FieldType = value;
            }
        }

        /// <summary>
        /// 是否唯一
        /// </summary>
        Boolean _IsKeyField = false;
        public Boolean IsKeyField
        {
            get
            {
                return _IsKeyField;
            }
            set
            {
                _IsKeyField = value;
            }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        Boolean _IsNotNull = false;
        public Boolean IsNotNull
        {
            get
            {
                return _IsNotNull;
            }
            set
            {
                _IsNotNull = value;
            }
        }

        /// <summary>
        /// 复制资料时是否复制
        /// 默认是复制
        /// </summary>
        Boolean _IsNotCopy = false;
        public Boolean IsNotCopy
        {
            get
            {
                return _IsNotCopy;
            }
            set
            {
                _IsNotCopy = value;
            }
        }

        /// <summary>
        /// 平行资料时是否复制
        /// </summary>
        Boolean _IsPingxing = false;
        public Boolean IsPingxing
        {
            get
            {
                return _IsPingxing;
            }
            set
            {
                _IsPingxing = value;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        Boolean _IsReadOnly = false;
        public Boolean IsReadOnly
        {
            get
            {
                return _IsReadOnly;
            }
            set
            {
                _IsReadOnly = value;
            }
        }

        public override string ToString()
        {
            return Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
