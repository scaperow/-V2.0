using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "RecordListElementList", ItemName = "RecordListElement", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class RecordListElementCollection : List<RecordListElement>
    {
        public RecordListElementCollection()
        {
        }

        public int IndexOfByCode(string Code)
        {
            int Result = -1;
            for (int i = 0; i < Count; i++)
            {
                if (Code.StartsWith(this[i].Code))
                {
                    Result = i;
                    break;
                }
            }

            return Result;
        }
    }
}
