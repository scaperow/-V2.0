using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class Role
    {
        PermissionCollection m_Permissions = new PermissionCollection();

        string m_Name;
        string m_Index;
        String m_Code;
        Boolean m_IsAdministrator;

        public Role()
        {
        }

        [DataMember]
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
        public String Code
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

        [DataMember]
        public Boolean IsAdministrator
        {
            get
            {
                return m_IsAdministrator;
            }
            set
            {
                m_IsAdministrator = value;
            }
        }

        [DataMember]
        public PermissionCollection Permissions
        {
            get
            {
                return m_Permissions;
            }
            set
            {
                m_Permissions = value;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Role role = obj as Role;
            if (role != null)
                return (string.Compare(role.Index, this.Index, true) == 0);
            return false;
        }
    }
}
