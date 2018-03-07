using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "RoleList", ItemName = "Role", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class RoleCollection : List<Role>
    {
    }
}
