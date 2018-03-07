using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class FunctionPermission
    {
        string m_Index;
        string m_Caption;

        [DataMember]
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

        [DataMember]
        public string Caption
        {
            get
            {
                return m_Caption;
            }
            set
            {
                m_Caption = value;
            }
        }
    }
}
