using System;
using System.Collections.Generic;
using System.Data;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;

namespace BizCommon
{
    /// <summary>
    /// 数据库表中的表信息
    /// </summary>
    [Serializable]
    public class TableDefineInfo
    {
        public TableDefineInfo()
        {
            Index = Guid.NewGuid().ToString();
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

        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        string _Description;
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_Description))
                    _Description = "数据表";

                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public string Type
        {
            get
            {
                return "BIZ";
            }
        }

        List<FieldDefineInfo> _FieldInfos = new List<FieldDefineInfo>();
        public List<FieldDefineInfo> FieldInfos
        {
            get
            {
                return _FieldInfos;
            }
            set
            {
                _FieldInfos = value;
            }
        }
        public string GetFieldList(List<FieldDefineInfo> Info)
        {
            if (Info == null)
                return "";

            List<string> Fields = new List<string>();
            foreach (FieldDefineInfo FieldInfo in Info)
            {
                Fields.Add("[" + FieldInfo.FieldName + "]");
            }

            return string.Join(",", Fields.ToArray());
        }
        public FieldDefineInfo GetFieldDefineInfo(string FieldName)
        {
            FieldDefineInfo fieldInfo = null;
            foreach (FieldDefineInfo FieldInfo in FieldInfos)
            {
                if (FieldInfo.FieldName.ToLower() == FieldName.ToLower())
                {
                    fieldInfo = FieldInfo;
                    break;
                }
            }

            return fieldInfo;
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
