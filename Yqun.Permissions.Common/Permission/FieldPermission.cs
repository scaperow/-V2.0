using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class FieldPermission
    {
        string m_Index;
        string m_FieldName;
        Boolean m_Editable;
        Boolean m_Viewable;

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

        [DataMember]
        public Boolean Editable
        {
            get
            {
                return m_Editable;
            }
            set
            {
                m_Editable = value;
            }
        }

        [DataMember]
        public Boolean Viewable
        {
            get
            {
                return m_Viewable;
            }
            set
            {
                m_Viewable = value;
            }
        }
    }
}
