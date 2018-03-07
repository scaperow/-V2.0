using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ReportDataItem
    {
        private String m_Index;
        public String Index
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

        private String m_Description;
        public String Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

    }
}
