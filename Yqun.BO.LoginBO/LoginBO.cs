using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Common.ContextCache;
using Yqun.Data.DataBase;
using Yqun.Common;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using Yqun.Common.Encoder;
using TransferServiceCommon;
using System.ServiceModel;

namespace Yqun.BO
{
    public class LoginBO : Yqun.BO.BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LoginBO()
        {

        }

        public LoginBO(string _DataAdapterType,
                            string _DataBaseType,
                            bool _ISIntegrated,
                            string _DataSource,
                            string _Instance,
                            string _UserName,
                            string _Password,
                            bool _ISAttach,
                            string _AppRoot
                            )
            : base(_DataAdapterType,
                    _DataBaseType,
                    _ISIntegrated,
                    _DataSource,
                    _Instance,
                    _UserName,
                    _Password,
                    _ISAttach,
                    _AppRoot
                    )
        {

        }

        public bool Login(string UserName, string Password)
        {
            try
            {
                return CheckUser(UserName, Password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckUser(string UserName, string Password)
        {
            DataSet ds = new DataSet();
            string SqlText = "select * from sys_auth_Users";
            string[] cols = new string[] { "UserName", "Password" };
            string[] exps = new string[] { "=", "=" };
            Password = EncryptSerivce.Encrypt(Password);
            object[] valus = new object[] { UserName, Password };
            ds = GetDataSet(SqlText, cols, exps, valus);
            if (ds.Tables[0].Rows.Count == 0)
            {
                object obj;

                try
                {
                    String moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";
                    //去模板库中验证管理员用户
                    obj = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.LoginBO.dll", "CheckAdminUser",
                                    new Object[] { UserName, Password });
                }
                catch
                {
                    obj = false;
                }
                return System.Convert.ToBoolean(obj);
            }
            else
            {
                return true;
            }
        }

        public bool CheckAdminUser(string UserName, string Password)
        {
            string sql = "select * from sys_user Where (GLActive=1 or CJActive=1) and IsDeleted!=1 and UserName=@0 and Password=@1 and RoleName='admin'";
            DataSet ds = GetDataSet(sql, new Object[] { UserName, Password });
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable GetAdminUserTable(String userName)
        {
            String sql = " select * from sys_user where (GLActive=1 or CJActive=1) and IsDeleted!=1 and UserName='" + userName + "' and RoleName='admin'";
            return GetDataTable(sql);
        }

        public DataTable GetRemoteAdminTable(String userName)
        {
            try
            {
                String moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";
                //去模板库中验证管理员用户
                return CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.LoginBO.dll", "GetAdminUserTable",
                                new Object[] { userName }) as DataTable;
            }
            catch
            {
                return null;
            }
        }

        private object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
        {
            object obj = null;
            try
            {
                using (ChannelFactory<ITransferService> channelFactory = new ChannelFactory<ITransferService>("sClient", new EndpointAddress(address)))
                {

                    ITransferService proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable["assembly_name"] = AssemblyName;
                        hashtable["method_name"] = MethodName;
                        hashtable["method_paremeters"] = Parameters;

                        Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                        Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream);
                        source_stream.Dispose();
                        Stream stream_result = proxy.InvokeMethod(zip_stream);
                        zip_stream.Dispose();
                        Stream ms = ReadMemoryStream(stream_result);
                        stream_result.Dispose();
                        Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                        ms.Dispose();
                        Hashtable Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;

                        obj = Result["return_value"];
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("call remote server method error: " + ex.Message);
            }
            return obj;
        }

        private MemoryStream ReadMemoryStream(Stream Params)
        {
            MemoryStream serviceStream = new MemoryStream();
            byte[] buffer = new byte[10000];
            int bytesRead = 0;
            int byteCount = 0;

            do
            {
                bytesRead = Params.Read(buffer, 0, buffer.Length);
                serviceStream.Write(buffer, 0, bytesRead);

                byteCount = byteCount + bytesRead;
            } while (bytesRead > 0);

            serviceStream.Position = 0;

            return serviceStream;
        }

        /// <summary>
        /// 登录拌合站
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool CheckUserBhz(string UserName, string Password)
        {
            DataSet ds = new DataSet();
            string SqlText = "select * from bhz_sys_BaseUsers";
            string[] cols = new string[] { "UserName", "Password" };
            string[] exps = new string[] { "=", "=" };
            Password = EncryptSerivce.Encrypt(Password);
            object[] valus = new object[] { UserName, Password };

            ds = GetDataSet(SqlText, cols, exps, valus);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 登录试验室管理系统
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool CheckUserSys(string UserName, string Password)
        {
            DataSet ds = new DataSet();
            string SqlText = "select * from sys_BaseUsers";
            string[] cols = new string[] { "UserName", "Password" };
            string[] exps = new string[] { "=", "=" };
            Password = EncryptSerivce.Encrypt(Password);
            object[] valus = new object[] { UserName, Password };

            ds = GetDataSet(SqlText, cols, exps, valus);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 测试网络连接
        /// </summary>
        /// <returns></returns>
        public bool TestNetwork()
        {
            return true;
        }

        public bool TestDbConnection()
        {
            string SqlText = "select * from sys_auth_Users where 1<>1";
            DataTable Data = GetDataTable(SqlText);
            return (Data != null && Data.Columns.Count > 0);
        }

        /// <summary>
        /// 客服工具登录方法
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool LoginForCustomer(string UserName, string Password)
        {
            DataTable dt = GetDataTable("select * from sys_auth_Customer_Service_Users where UserName='" + UserName + "' and UserPwd='" + Password + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 客服工具修密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool ChangPWdForCustomer(string UserName, string NewPwd)
        {
            if (ExcuteCommand("update sys_auth_Customer_Service_Users set UserPwd='" + NewPwd + "' where UserName='" + UserName + "'") > 0)
            {
                return true;
            }

            return false;
        }


        public string LoginMsg(string UserName, string UserPassword, string MachineCode)
        {
            DataTable dt = GetUserInfo(UserName);
            if (dt == null)
            {
                return "用户不存在，请确认用户名正确后重新登录！";
            }
            else if (dt.Rows.Count <= 0)
            {
                object temp = GetRemoteAdminTable(UserName);
                if (temp == null)
                {
                    return "用户不存在，请确认用户名正确后重新登录！";
                }
                else
                {

                    dt = temp as DataTable;
                    if (dt.Rows.Count <= 0)
                    {
                        return "用户不存在，请确认用户名正确后重新登录！";
                    }
                    else if (dt.Rows.Count >= 2)
                    {
                        return "系统中存在多条用户信息，请联系管理员！";
                    }
                    else
                    {
                        if (EncryptSerivce.Encrypt(UserPassword) != dt.Rows[0]["Password"].ToString())
                        {
                            return "密码错误，请确认正确后重新登录！";
                        }
                    }
                }
            }
            else if (dt.Rows.Count >= 2)
            {
                return "系统中存在多条用户信息，请联系管理员！";
            }
            //else if (dt.Rows[0]["Scdel"].ToString() == "1")
            //{
            //    return "该用户信息已被删除，请联系管理员处理后重新登录！";
            //}
            else
            {
                if (EncryptSerivce.Encrypt(UserPassword) != dt.Rows[0]["Password"].ToString())
                {
                    return "密码错误，请确认正确后重新登录！";
                }
                else if (MachineCode.Length != 20)
                {
                    return "采集系统设备编码配置有误，请联系管理重新配置后重新登录！";
                }
                else
                {
                    if (dt.Rows[0]["Code"].ToString().Trim().Length > 16)
                    {
                        if (dt.Rows[0]["Code"].ToString().Trim().Substring(0, 16) != MachineCode.Trim().Substring(0, 16) && dt.Rows[0]["Devices"].ToString().Trim().IndexOf(MachineCode) < 0)
                        {
                            return "用户无权限操作此设备！";
                        }
                    }
                    else if (dt.Rows[0]["Code"].ToString().Trim().Length != 20 && dt.Rows[0]["Code"].ToString().Trim().Length != 8)
                    {
                        return "用户编码有误，请联系管理员！";
                    }
                }
            }
            return "true";
        }

        public DataTable GetUserInfo(string UserName)
        {
            DataSet ds = new DataSet();
            string SqlText = "select * from sys_auth_Users";
            string[] cols = new string[] { "UserName", "Scdel" };
            string[] exps = new string[] { "=", "=" };
            object[] valus = new object[] { UserName, "0" };
            ds = GetDataSet(SqlText, cols, exps, valus);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime()
        {
            return DateTime.Now;
        }

    }
}
