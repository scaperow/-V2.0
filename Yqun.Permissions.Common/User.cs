using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions.Common
{
    [Serializable]
    public class User : IRole
    {
        public User()
        {
        }

        string m_Index;
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

        string m_Name;
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

        string m_Password;
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
            }
        }

        RoleCollection m_Roles = new RoleCollection();
        public RoleCollection Roles
        {
            get
            {
                return m_Roles;
            }
            set
            {
                m_Roles = value;
            }
        }

        Boolean m_IsSys;
        public Boolean IsSys
        {
            get
            {
                return m_IsSys;
            }
            set
            {
                m_IsSys = value;
            }
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user != null)
                return (string.Compare(user.Name, this.Name,true) == 0);
            return false;
        }
    }
}
