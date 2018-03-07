using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    /// <summary>
    /// 表的所有条件
    /// </summary>
    [Serializable]
    [DataContract]
    public class DatasPermission : Permission
    {
        DataPermissionCollection m_Conditions = new DataPermissionCollection();

        [DataMember]
        public DataPermissionCollection Conditions
        {
            get
            {
                return m_Conditions;
            }
            set
            {
                m_Conditions = value;
            }
        }

        /// <summary>
        /// 获得相同表的相同字段的的所有相同值
        /// </summary>
        /// <param name="dataPermission"></param>
        /// <returns></returns>
        public DatasPermission Intersect(DatasPermission datasPermission)
        {
            DataPermissionCollection Result = new DataPermissionCollection();
            if (this.Caption == datasPermission.Caption)
            {
                foreach (DataPermission thisData in this.Conditions)
                {
                    foreach (DataPermission data in datasPermission.Conditions)
                    {
                        DataPermission datapermission = thisData.Intersect(data);
                        if (datapermission != null)
                            Result.Add(datapermission);
                    }
                }
            }

            if (Result.Count > 0)
            {
                DatasPermission data = new DatasPermission();
                data.Caption = this.Caption;
                foreach (DataPermission d in Result)
                    data.Conditions.Add(d);

                return data;
            }

            return null;
        }
    }
}
