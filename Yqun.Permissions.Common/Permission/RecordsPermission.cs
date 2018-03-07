using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class RecordsPermission : Permission
    {
        RecordListElementCollection m_RecordPermissionList = new RecordListElementCollection();

        [DataMember]
        public RecordListElementCollection RecordPermissionList
        {
            get
            {
                return m_RecordPermissionList;
            }
            set
            {
                m_RecordPermissionList = value;
            }
        }
    }
}
