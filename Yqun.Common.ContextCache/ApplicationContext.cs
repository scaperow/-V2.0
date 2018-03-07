using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;
using Yqun.Permissions.Common;
using System.Runtime.Serialization;
using Yqun.Bases;

namespace Yqun.Common.ContextCache
{
    [KnownType(typeof(RoleCollection))]
    [KnownType(typeof(ObjectInfo))]
    [KnownType(typeof(IdentificationInfo))]
    [CollectionDataContract(Namespace = "http://www.yqunsoft.com/Railway/", ItemName = "Context", KeyName = "Key", ValueName = "Value")]
    public class ApplicationContext : Dictionary<string, object>
    {
        public const string ContextKey = "__applicationContext";
        public const string ContextHeaderLocalName = "ApplicationContext";
        public const string ContextHeaderNamespace = "http://www.yqunsoft.com/Railway/";
        public static ApplicationContext Current
        {
            get
            {
                if (CallContext.GetData(ContextKey) == null)
                {
                    CallContext.SetData(ContextKey, new ApplicationContext());
                }

                return (ApplicationContext)CallContext.GetData(ContextKey);
            }

            set
            {
                CallContext.SetData(ContextKey, value);
            }
        }

        public string AppIndex
        {
            get
            {
                if (!this.ContainsKey("__AppIndex"))
                {
                    return string.Empty;
                }

                return (string)this["__AppIndex"];
            }

            set
            {
                this["__AppIndex"] = value;
            }
        }

        public string SheetTitle
        {
            get
            {
                if (!this.ContainsKey("__SheetTitle"))
                {
                    return string.Empty;
                }

                return (string)this["__SheetTitle"];
            }

            set
            {
                this["__SheetTitle"] = value;
            }
        }

        public IdentificationInfo Identification
        {
            get
            {
                if (!this.ContainsKey("__Identification"))
                {
                    return IdentificationInfo.Empty;
                }

                return (IdentificationInfo)this["__Identification"];
            }

            set
            {
                this["__Identification"] = value;
            }
        }

        public ObjectInfo InProject
        {
            get
            {
                if (!this.ContainsKey("__Project"))
                {
                    return ObjectInfo.Empty;
                }

                return (ObjectInfo)this["__Project"];
            }

            set
            {
                this["__Project"] = value;
            }
        }

        public ObjectInfo InSegment
        {
            get
            {
                if (!this.ContainsKey("__Segment"))
                {
                    return ObjectInfo.Empty;
                }

                return (ObjectInfo)this["__Segment"];
            }

            set
            {
                this["__Segment"] = value;
            }
        }

        public ObjectInfo InCompany
        {
            get
            {
                if (!this.ContainsKey("__Company"))
                {
                    return ObjectInfo.Empty;
                }

                return (ObjectInfo)this["__Company"];
            }

            set
            {
                this["__Company"] = value;
            }
        }

        public ObjectInfo InTestRoom
        {
            get
            {
                if (!this.ContainsKey("__LabRoom"))
                {
                    return ObjectInfo.Empty;
                }

                return (ObjectInfo)this["__LabRoom"];
            }

            set
            {
                this["__LabRoom"] = value;
            }
        }

        public string UserName
        {
            get
            {
                if (!this.ContainsKey("__UserName" ))
                {
                    return string.Empty;
                }
                
                return (string)this["__UserName"];
            }
            
            set
            {
                this["__UserName"] = value;
            }
        }

        public string UserCode
        {
            get
            {
                if (!this.ContainsKey("__UserCode"))
                {
                    return string.Empty;
                }

                return (string)this["__UserCode"];
            }

            set
            {
                this["__UserCode"] = value;
            }
        }
        public string DeniedModuleIDs
        {
            get
            {
                if (!this.ContainsKey("__DeniedModuleIDs"))
                {
                    return string.Empty;
                }

                return (string)this["__DeniedModuleIDs"];
            }

            set
            {
                this["__DeniedModuleIDs"] = value;
            }
        }
        public string Password
        {
            get
            {
                if (!this.ContainsKey("__Password"))
                {
                    return string.Empty;
                }

                return (string)this["__Password"];
            }

            set
            {
                this["__Password"] = value;
            }
        }

        public Boolean IsSystemUser
        {
            get
            {
                if (!this.ContainsKey("__IsSystemUser"))
                {
                    return false;
                }

                return (Boolean)this["__IsSystemUser"];
            }

            set
            {
                this["__IsSystemUser"] = value;
            }
        }

        public RoleCollection Roles
        {
            get
            {
                if (!this.ContainsKey("__Roles"))
                {
                    return new RoleCollection();
                }

                return (RoleCollection)this["__Roles"];
            }

            set
            {
                this["__Roles"] = value;
            }
        }

        public Boolean IsAdministrator
        {
            get
            {
                Boolean Result = IsSystemUser;
                foreach (Role role in Roles)
                {
                    if (role.IsAdministrator)
                    {
                        Result = true;
                        break;
                    }
                }

                return Result;
            }
        }

        public Boolean ISLocalService
        {
            get
            {
                if (!this.ContainsKey("__ISLocalService"))
                {
                    return false;
                }

                return (Boolean)this["__ISLocalService"];
            }

            set
            {
                this["__ISLocalService"] = value;
            }
        }

        public string LocalStartPath
        {
            get
            {
                if (!this.ContainsKey("__LocalStartPath"))
                {
                    return string.Empty;
                }

                return (string)this["__LocalStartPath"];
            }

            set
            {
                this["__LocalStartPath"] = value;
            }
        }

        Boolean _IsServer = false;
        public Boolean IsServer
        {
            get
            {
                return _IsServer;
            }
            set
            {
                _IsServer = value;
            }
        }



        private string _MachineCode;
        /// <summary>
        /// 设计用户可操作用户集合
        /// </summary>
        public string MachineCode
        {
            set
            {
                _MachineCode = value;
                List<string> listtemp = new List<string>();
                foreach (string  item in value.Split(','))
                {
                    listtemp.Add(item);
                }
                _MachineCodeList = listtemp;
            }
            get
            {
                return _MachineCode;
            }
        }

        private List<string> _MachineCodeList;

        /// <summary>
        /// 获取用户可操作设备集合
        /// </summary>
        public List<string> GetMachineCode
        {
            get { return _MachineCodeList; }
        }
    }
}
