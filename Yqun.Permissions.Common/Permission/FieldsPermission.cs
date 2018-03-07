using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class FieldsPermission : Permission
    {
        string _FieldsName;
        FieldPermissionCollection m_Fields = new FieldPermissionCollection();

        [DataMember]
        public FieldPermissionCollection Fields
        {
            get
            {
                return m_Fields;
            }
            set
            {
                m_Fields = value;
            }
        }

        [DataMember]
        public string FieldsName
        {
            get
            {
                return _FieldsName;
            }
            set
            {
                _FieldsName = value;
            }
        }
    }
}
