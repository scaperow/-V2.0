using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "RecordPermissionList", ItemName = "RecordPermission", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class RecordPermissionCollection : List<RecordPermission>
    {
    }
}
