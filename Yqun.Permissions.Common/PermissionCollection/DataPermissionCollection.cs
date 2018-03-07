using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "DataPermissionList", ItemName = "DataPermissionEntry", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class DataPermissionCollection : List<DataPermission>
    {
    }
}
