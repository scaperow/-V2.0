using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "FieldPermissionList", ItemName = "FieldPermission", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class FieldPermissionCollection : List<FieldPermission>
    {
    }
}
