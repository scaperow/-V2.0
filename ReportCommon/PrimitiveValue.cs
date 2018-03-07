using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ReportCommon
{
    [Serializable]
    public class PrimitiveValue : ISerializable
    {
        public static PrimitiveValue NULL = new PrimitiveValue();

        public PrimitiveValue()
        {
        }

        protected PrimitiveValue(SerializationInfo info, StreamingContext context)
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}
