using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions.Common
{
    public class TableInfo
    {
        string m_TableIndex;
        public string TableIndex
        {
            get
            {
                return m_TableIndex;
            }
            set
            {
                m_TableIndex = value;
            }
        }

        string m_TableText;
        public string TableText
        {
            get
            {
                return m_TableText;
            }
            set
            {
                m_TableText = value;
            }
        }
        string m_TableName;
        public string TableName
        {
            get
            {
                return m_TableName;
            }
            set
            {
                m_TableName = value;
            }
        }

        ColumnInfoCollection m_ColumnsInfo = new ColumnInfoCollection();
        public ColumnInfoCollection ColumnsInfo
        {
            get
            {
                return m_ColumnsInfo;
            }
        }
    }
}
