using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ReportCommon
{
    [Serializable]
    public class ReportConfiguration : ISerializable
    {
        public ReportConfiguration()
        {
        }

        //反序列化
        protected ReportConfiguration(SerializationInfo info, StreamingContext context)
        {
            Index = info.GetString("Index");
            Code = info.GetString("Code");
            Description = info.GetString("Description");
            SheetStyle = info.GetString("SheetStyle");
            DataSources = info.GetValue("TableDataCollection", typeof(TableDataCollection)) as TableDataCollection;
            ReportParameters = info.GetValue("ReportParameters", typeof(List<ReportParameter>)) as List<ReportParameter>;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Index", Index);
            info.AddValue("Code", Code);
            info.AddValue("Description", Description);
            info.AddValue("SheetStyle", SheetStyle);
            info.AddValue("TableDataCollection", DataSources);
            info.AddValue("ReportParameters", ReportParameters);
        }

        /// <summary>
        /// 报表的索引
        /// </summary>
        String _Index = Guid.NewGuid().ToString();
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

        /// <summary>
        /// 报表的编码
        /// </summary>
        string m_Code;
        public string Code
        {
            get
            {
                return m_Code;
            }
            set
            {
                m_Code = value;
            }
        }

        /// <summary>
        /// 报表的描述
        /// </summary>
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

        /// <summary>
        /// 报表的样式
        /// </summary>
        string m_SheetStyle;
        public string SheetStyle
        {
            get
            {
                return m_SheetStyle;
            }
            set
            {
                m_SheetStyle = value;
            }
        }

        /// <summary>
        /// 与报表相关的数据源,支持多数据源
        /// </summary>
        TableDataCollection _DataSources = new TableDataCollection();
        public TableDataCollection DataSources
        {
            get
            {
                return _DataSources;
            }
            set
            {
                _DataSources = value;
            }
        }

        List<ReportParameter> reportParameters = new List<ReportParameter>();
        public List<ReportParameter> ReportParameters
        {
            get
            {
                return reportParameters;
            }
            set
            {
                reportParameters = value;
            }
        }
    }
}
