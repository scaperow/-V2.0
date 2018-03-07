using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Common.ContextCache
{
    [Serializable]
    [DataContract]
    public class IdentificationInfo
    {
        public static IdentificationInfo Empty = new IdentificationInfo();

        String _MachineName;
        String _MacAddress;
        String _OSVersion;
        String _UserName;

        [DataMember]
        public String MachineName
        {
            get
            {
                return _MachineName;
            }
            set
            {
                _MachineName = value;
            }
        }

        [DataMember]
        public String MacAddress
        {
            get
            {
                return _MacAddress;
            }
            set
            {
                _MacAddress = value;
            }
        }

        [DataMember]
        public String OSVersion
        {
            get
            {
                return _OSVersion;
            }
            set
            {
                _OSVersion = value;
            }
        }

        [DataMember]
        public String UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        [DataMember]
        public String IPAddress
        {
            get;
            set;
        }
    }
}
