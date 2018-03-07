using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class FunctionsPermission : Permission
    {
        FunctionPermissionCollection m_Functions = new FunctionPermissionCollection();

        [DataMember]
        public FunctionPermissionCollection Functions
        {
            get
            {
                return m_Functions;
            }
            set
            {
                m_Functions = value;
            }
        }
    }
}
