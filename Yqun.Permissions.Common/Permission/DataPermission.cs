using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    /// <summary>
    /// 字段条件
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataPermission
    {
        string m_Index = Guid.NewGuid().ToString();
        List<string> m_Values = new List<string>();
        String m_FieldName;

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
        public List<string> Values
        {
            get
            {
                return m_Values;
            }
            set
            {
                m_Values = value;
            }
        }

        [DataMember]
        public String FieldName
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

        /// <summary>
        /// 获得相同字段的所有相同值
        /// </summary>
        /// <param name="dataPermission"></param>
        /// <returns></returns>
        public DataPermission Intersect(DataPermission dataPermission)
        {
            List<string> list = new List<string>();
            if (this.m_FieldName == dataPermission.m_FieldName)
            {
                for (int i = 0; i < this.Values.Count; i++)
                {
                    if (this.Values.Contains(dataPermission.Values[i]))
                        list.Add(dataPermission.Values[i]);
                }

            }

            if (list.Count > 0)
            {
                DataPermission data = new DataPermission();
                data.FieldName = this.FieldName;
                data.Values.AddRange(list);
                return data;
            }

            return null;
        }
    }
}
