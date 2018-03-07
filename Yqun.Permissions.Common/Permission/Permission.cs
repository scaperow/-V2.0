using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    /// <summary>
    /// 权限点 
    /// </summary>
    [Serializable]
    [KnownType(typeof(RecordsPermission))]
    [KnownType(typeof(FieldsPermission))]
    [KnownType(typeof(FunctionsPermission))]
    [KnownType(typeof(DatasPermission))]
    [DataContract]
    public class Permission
    {
        string m_ModuleID;
        string m_Caption;
        string m_Index;


        /// <summary>
        /// 权限所属模块
        /// </summary>
        [DataMember]
        public string ModuleID
        {
            get
            {
                return m_ModuleID;
            }
            set
            {
                m_ModuleID = value;
            }
        }

        /// <summary>
        /// 权限点的描述
        /// </summary>
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

        /// <summary>
        /// 权限点的Index
        /// </summary>
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
    }
}
