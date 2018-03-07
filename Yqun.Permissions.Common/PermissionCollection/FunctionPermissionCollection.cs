using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "FunctionPermissionList", ItemName = "FunctionPermission", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class FunctionPermissionCollection : List<FunctionPermission>
    {
        public FunctionPermissionCollection()
        {
        }

        public int IndexOf(string Index)
        {
            int Result = -1;
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Index == Index)
                {
                    Result = i;
                    break;
                }
            }

            return Result;
        }
    }
}
