using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Yqun.Permissions.Common
{
    [Serializable]
    [CollectionDataContract(Name = "PermissionList", ItemName = "Permission", Namespace = "http://www.yqunsoft.com/SSYWGL/")]
    public class PermissionCollection : List<Permission>
    {
        public PermissionCollection()
        {
        }

        public int IndexOfByTreeName(string TreeName)
        {
            return IndexofByName(TreeName, PermissionType.Records);
        }

        public int IndexOfFields(string TableName)
        {
            return IndexofByName(TableName, PermissionType.Fields);
        }

        public int IndexOfByTableName(string TableName)
        {
            return IndexofByName(TableName, PermissionType.Datas);
        }

        private int IndexofByName(string Name, PermissionType Type)
        {
            int Result = -1;
            switch (Type)
            {
                case PermissionType.Functions:
                    for (int i = 0; i < Count; i++)
                    {
                        if (this[i].Caption == Name && this[i] is FunctionsPermission)
                        {
                            Result = i;
                            break;
                        }
                    }
                    break;
                case PermissionType.Fields:
                    for (int i = 0; i < Count; i++)
                    {
                        if (this[i].Caption == Name && this[i] is FieldsPermission)
                        {
                            Result = i;
                            break;
                        }
                    }
                    break;
                case PermissionType.Datas:
                    for (int i = 0; i < Count; i++)
                    {
                        if (this[i].Caption == Name && this[i] is DatasPermission)
                        {
                            Result = i;
                            break;
                        }
                    }
                    break;
                case PermissionType.Records:
                    for (int i = 0; i < Count; i++)
                    {
                        if (this[i].Caption == Name && this[i] is RecordsPermission)
                        {
                            Result = i;
                            break;
                        }
                    }
                    break;
            }

            return Result;
        }

        public int IndexOfByModuleID(string ModuleID)
        {
            int Result = -1;
            for (int i = 0; i < Count; i++)
            {
                if (this[i].ModuleID == ModuleID)
                {
                    Result = i;
                    break;
                }
            }

            return Result;
        }
    }
}
