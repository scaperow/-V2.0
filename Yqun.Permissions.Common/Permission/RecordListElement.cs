﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [DataContract]
    public class RecordListElement
    {
        string m_Index;
        string m_Caption;
        string m_Code;

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

        [DataMember]
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
    }
}