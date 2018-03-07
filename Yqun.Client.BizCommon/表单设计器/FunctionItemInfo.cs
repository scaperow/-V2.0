using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class FunctionItemInfo
    {
        public FunctionItemInfo()
        {
            m_Index = Guid.NewGuid().ToString();
        }

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

        private String m_AssemblyName;
        public String AssemblyName
        {
            get
            {
                return m_AssemblyName;
            }
            set
            {
                m_AssemblyName = value;
            }
        }

        private String m_FullClassName;
        public String FullClassName
        {
            get
            {
                return m_FullClassName;
            }
            set
            {
                m_FullClassName = value;
            }
        }
    }
}
