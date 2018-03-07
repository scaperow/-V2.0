using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Common.ContextCache
{
    [Serializable]
    [DataContract]
    public class ObjectInfo
    {
        public static ObjectInfo Empty = new ObjectInfo();

        String _Index = "";
        String _Code = "";
        String _Type = "";
        String _Description = "";

        [DataMember]
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        [DataMember]
        public String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        [DataMember]
        public String Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        [DataMember]
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
