using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TransferServiceCommon;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using Yqun.Common.ContextCache;
using System.Configuration;
using Yqun.Common.Encoder;
using System.Collections;
using System.Diagnostics;
using System.ServiceModel.Channels;
using Yqun.Common;
using Yqun.Data.DataBase;
using System.Data;
using System.ServiceModel.Activation;
using System.Web;
using System.Threading;
using Newtonsoft.Json;

namespace WebTransferService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class TransferService : ITransferService
    {
        //缓存用到的所有类型信息
        static IDictionary<string, Type> typeList = new Dictionary<string, Type>();
        //缓存所有用到的方法信息
        static IDictionary<string, MethodInfo> methodList = new Dictionary<string, MethodInfo>();

        private static object syncRoot = new object();

        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static TransferService()
        {
            //读取配置信息
            String DataAdapterType = ConfigurationManager.AppSettings["DataAdapterType"];
            String DataBaseType = ConfigurationManager.AppSettings["DataBaseType"];
            String DataSource = ConfigurationManager.AppSettings["DataSource"];
            String DataInstance = ConfigurationManager.AppSettings["DataInstance"];
            String DataUserName = ConfigurationManager.AppSettings["DataUserName"];
            String DataPassword = ConfigurationManager.AppSettings["DataPassword"];
            String DataISAttach = ConfigurationManager.AppSettings["DataISAttach"];
            String AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];

            ServerLoginInfos.DBConnectionInfo.DataAdapterType = DataAdapterType;
            ServerLoginInfos.DBConnectionInfo.DataBaseType = DataBaseType;
            ServerLoginInfos.DBConnectionInfo.DataSource = EncryptSerivce.Dencrypt(DataSource);
            ServerLoginInfos.DBConnectionInfo.DataInstance = EncryptSerivce.Dencrypt(DataInstance);
            ServerLoginInfos.DBConnectionInfo.DataUserName = EncryptSerivce.Dencrypt(DataUserName);
            ServerLoginInfos.DBConnectionInfo.DataPassword = EncryptSerivce.Dencrypt(DataPassword);
            ServerLoginInfos.DBConnectionInfo.DataISAttach = DataISAttach;
            ServerLoginInfos.DBConnectionInfo.LocalStartPath = AppDomain.CurrentDomain.BaseDirectory + AssemblyPath.Trim("\\".ToCharArray()) + @"\";

            log4net.Config.XmlConfigurator.Configure();
            //LocalQuartzService.GetQuartzService().Start();
        }

        /// <summary>
        /// 从输入流中读出内存流
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        private MemoryStream ReadMemoryStream(Stream Params)
        {
            MemoryStream serviceStream = new MemoryStream();
            byte[] buffer = new byte[10000];
            int bytesRead = 0;

            do
            {
                bytesRead = Params.Read(buffer, 0, buffer.Length);
                serviceStream.Write(buffer, 0, bytesRead);

            } while (bytesRead > 0);

            serviceStream.Position = 0;

            return serviceStream;
        }

        #region ITransferService 成员

        public Stream InvokeMethod(Stream Params)
        {
            String ipAddress = "";
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            if (properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                ipAddress = string.Format("{0}:{1}", endpoint.Address, endpoint.Port);
            }
            List<string> parameters = new List<string>();

            try
            {
                ApplicationContext AppContext = ApplicationContext.Current;
                AppContext.Identification.IPAddress = ipAddress;
                LoginLog(AppContext);
            }
            catch (Exception ex)
            {
                logger.Error("log login:" + ex.Message);
            }
            object[] Method_Paremeters = null;
            string Method_Name = "";
            try
            {
                Stream ms = ReadMemoryStream(Params);
                //Params.Dispose();
                Stream unzipstream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                //ms.Dispose();
                Hashtable paramsList = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzipstream) as Hashtable;
                //unzipstream.Dispose();
                string path = ServerLoginInfos.DBConnectionInfo.LocalStartPath;
                parameters.Add(path);
                string Assembly_Name = paramsList["assembly_name"].ToString();
                parameters.Add(Assembly_Name);
                string FileName = Path.Combine(path.Trim(), Assembly_Name.Trim());
                parameters.Add(FileName);
                Method_Name = paramsList["method_name"].ToString();
                Method_Paremeters = paramsList["method_paremeters"] as object[];
                object o = InvokeMethod(FileName, Method_Name, Method_Paremeters);
                Hashtable t = new Hashtable();
                t.Add("return_value", o);

                Stream stream = Serialize.SerializeToStream(t);
                Stream zipstream = Compression.CompressStream(stream);
                //stream.Dispose();
                return zipstream;
            }
            catch (Exception ex)
            {
                String log = "";
                foreach (var item in Method_Paremeters)
                {
                    log += item.ToString() + ";";
                }


                logger.Error(string.Format("[{0}]访问服务出错，原因为“{1}”，参数列表为{2}, 传入参数为{3},方法名称{4}",
                    ApplicationContext.Current.UserName, ex.Message,
                    string.Join(",", parameters.ToArray()),
                    log,
                    Method_Name
                    ));
            }

            return null;
        }

        #endregion

        object InvokeMethod(object[] ConnectionArgs, string FileName, string MethodName, object[] Args)
        {
            try
            {
                Type[] types = new Type[Args.Length];
                for (int i = 0; i < types.Length; i++)
                {
                    if (Args[i] != null)
                    {
                        types[i] = Args[i].GetType();
                    }
                    else
                    {
                        types[i] = Type.GetType("System.Object");
                    }
                }

                Type type = GetType(FileName, MethodName, types);
                MethodInfo methodInfo = GetMethod(MethodName, type, types);
                object obj = null;
                if (!methodInfo.IsStatic)
                    obj = Activator.CreateInstance(type, ConnectionArgs);
                FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(methodInfo);
                return fastInvoker(obj, Args);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}]访问方法InvokeMethod(object[] ConnectionArgs, string FileName, string MethodName, object[] Args)出错，原因为“{1}”，参数列表：{2}，{3}", ApplicationContext.Current.UserName, ee.Message, FileName, MethodName));
                return null;
            }
        }

        object InvokeMethod(string FileName, string MethodName, object[] Args)
        {
            String errorPara = "";
            try
            {
                Type type = null;
                Int32 repeat = 0;
                Type[] types = new Type[Args.Length];

                while (type == null && repeat < 3)
                {
                    repeat++;
                    if (repeat > 1)
                    {
                        Thread.Sleep(300);
                    }
                    for (int i = 0; i < types.Length; i++)
                    {
                        if (Args[i] != null)
                        {
                            types[i] = Args[i].GetType();
                        }
                        else
                        {
                            types[i] = Type.GetType("System.Object");
                        }
                        errorPara += ", " + types[i].ToString();
                    }
                    type = GetType(FileName, MethodName, types);
                }

                MethodInfo methodInfo = GetMethod(MethodName, type, types);
                object obj = null;
                if (!methodInfo.IsStatic)
                    obj = Activator.CreateInstance(type, false);
                FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(methodInfo);
                object result = fastInvoker(obj, Args);

                return result;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("[{0}]访问方法object InvokeMethod(string FileName, string MethodName, object[] Args)出错，原因为“{1}”，参数列表：{2}，{3}, Para={4}",
                    ApplicationContext.Current.UserName, ee.Message, FileName, MethodName, errorPara));
                return null;
            }

        }

        Type FindType(string filename, string methodname, params Type[] paramtypes)
        {
            Assembly a = Assembly.LoadFrom(filename);
            Type[] types = a.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (!types[i].IsPublic) continue;

                MethodInfo methodInfo = types[i].GetMethod(methodname, paramtypes);
                if (methodInfo != null)
                {
                    return types[i];
                }
            }

            logger.Error(string.Format("FindType(string filename, string methodname, params Type[] paramtypes)，原因是方法 {0} 所属的类型没找到！", methodname));
            return null;
        }

        private Type GetType(string filename, string methodname, params Type[] types)
        {
            Type type = null;

            if (!typeList.ContainsKey(filename + methodname))
            {
                lock (syncRoot)
                {
                    if (!typeList.ContainsKey(filename + methodname))
                    {
                        type = FindType(filename, methodname, types);
                        typeList.Add(filename + methodname, type);
                    }
                }
            }
            else
                type = typeList[filename + methodname];

            return type;
        }

        private MethodInfo GetMethod(string MethodName, Type type, params Type[] types)
        {
            MethodInfo methodInfo = null;
            string MethodKey = MethodName;
            for (int i = 0; i < types.Length; i++)
            {
                MethodKey = MethodKey + "_" + types[i].ToString();
            }

            if (!methodList.ContainsKey(MethodKey))
            {
                lock (syncRoot)
                {
                    if (!methodList.ContainsKey(MethodKey))
                    {
                        methodInfo = type.GetMethod(MethodName, types);
                        methodList.Add(MethodKey, methodInfo);
                    }
                }
            }
            else
                methodInfo = methodList[MethodKey];

            return methodInfo;
        }

        private void LoginLog(ApplicationContext context)
        {
            if (context == null || context.Identification == null || ServerLoginInfos.DBConnectionInfo == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(context.Identification.MacAddress) || String.IsNullOrEmpty(context.UserName))
            {
                return;
            }
            if (context.IsAdministrator)
            {
                return;
            }

            String today = DateTime.Now.ToString("yyyy-MM-dd");
            String sql = String.Format("SELECT UserName FROM dbo.sys_loginlog WHERE loginDay='{0}' AND macAddress='{1}' AND UserName='{2}'",
                today,
                context.Identification.MacAddress,
                context.UserName);
            try
            {
                bool DataISAttach = false;
                bool.TryParse(ServerLoginInfos.DBConnectionInfo.DataISAttach, out DataISAttach);
                bool DataIntegratedLogin = false;
                bool.TryParse(ServerLoginInfos.DBConnectionInfo.DataIntegratedLogin, out DataIntegratedLogin);

                DataService Service = new DataService(ServerLoginInfos.DBConnectionInfo.DataAdapterType,
                        ServerLoginInfos.DBConnectionInfo.DataBaseType,
                        DataIntegratedLogin,
                        ServerLoginInfos.DBConnectionInfo.DataSource,
                        ServerLoginInfos.DBConnectionInfo.DataInstance,
                        ServerLoginInfos.DBConnectionInfo.DataUserName,
                        ServerLoginInfos.DBConnectionInfo.DataPassword,
                        DataISAttach,
                        ServerLoginInfos.DBConnectionInfo.LocalStartPath);
                //logger.Error("datasource=" + ServerLoginInfos.DBConnectionInfo.DataSource + ", DataInstance=" + ServerLoginInfos.DBConnectionInfo.DataInstance);
                DataTable tb = Service.GetDataTable(sql);
                //logger.Error("sql=" + sql);
                if (tb != null)
                {
                    if (tb.Rows.Count > 0)
                    {
                        string updateSql = String.Format(@"UPDATE dbo.sys_loginlog SET 
                            ipAddress='{0}', 
                            machineName='{1}', 
                            osVersion='{2}', 
                            osUserName='{3}', 
                            ProjectName='{4}', 
                            SegmentName='{5}', 
                            CompanyName='{6}',
                            TestRoomName='{7}', 
                            TestRoomCode='{12}',
                            LastAccessTime='{8}'
                            WHERE loginDay='{9}' AND macAddress='{10}' AND UserName='{11}'",
                            context.Identification.IPAddress,
                            context.Identification.MachineName,
                            context.Identification.OSVersion,
                            context.Identification.UserName,
                            context.InProject.Description,
                            context.InSegment.Description,
                            context.InCompany.Description,
                            context.InTestRoom.Description,
                            DateTime.Now,
                            today,
                            context.Identification.MacAddress,
                            context.UserName,
                            context.InTestRoom.Code);
                        Service.ExcuteCommand(updateSql);
                    }
                    else
                    {
                        String insertSql = String.Format(@"INSERT INTO dbo.sys_loginlog
                            ( loginDay ,
                              ipAddress ,
                              macAddress ,
                              machineName ,
                              osVersion ,
                              osUserName ,
                              UserName ,
                              ProjectName ,
                              SegmentName ,
                              CompanyName ,
                              TestRoomName ,
                              TestRoomCode,
                              FirstAccessTime ,
                              LastAccessTime
                            )
                            VALUES  ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{13}', '{11}', '{12}' )",
                           today,
                           context.Identification.IPAddress,
                           context.Identification.MacAddress,
                           context.Identification.MachineName,
                           context.Identification.OSVersion,
                           context.Identification.UserName,
                           context.UserName,
                           context.InProject.Description,
                           context.InSegment.Description,
                           context.InCompany.Description,
                           context.InTestRoom.Description,
                           DateTime.Now,
                           DateTime.Now,
                           context.InTestRoom.Code);
                        Service.ExcuteCommand(insertSql);
                    }
                }
                else
                {
                    logger.Error("记录登录日志时未链接到数据库");
                }
            }
            catch (Exception ex)
            {
                logger.Error("登录时记录日志异常：" + ex.Message);
            }
        }
    }

    /// <summary>
    /// 快速反射类
    /// </summary>
    /// <example>
    /// Type t = typeof(String);
    /// MethodInfo methodInfo = t.GetMethod("IsNullOrEmpty");
    /// object[] param = new object[] {"hello!"};
    /// FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(methodInfo);
    /// bool Result = fastInvoker(null, param);
    /// </example>
    public class FastInvoke
    {
        public delegate object FastInvokeHandler(object target, object[] paramters);

        static object InvokeMethod(FastInvokeHandler invoke, object target, params object[] paramters)
        {
            return invoke(null, paramters);
        }

        public static FastInvokeHandler GetMethodInvoker(MethodInfo methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, methodInfo.DeclaringType.Module);

            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = methodInfo.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    paramTypes[i] = ps[i].ParameterType.GetElementType();
                else
                    paramTypes[i] = ps[i].ParameterType;
            }

            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i], true);
            }

            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }

            if (!methodInfo.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    il.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    il.Emit(OpCodes.Ldloc, locals[i]);
            }
            if (methodInfo.IsStatic)
                il.EmitCall(OpCodes.Call, methodInfo, null);
            else
                il.EmitCall(OpCodes.Callvirt, methodInfo, null);
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, methodInfo.ReturnType);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                {
                    il.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(il, i);
                    il.Emit(OpCodes.Ldloc, locals[i]);
                    if (locals[i].LocalType.IsValueType)
                        il.Emit(OpCodes.Box, locals[i].LocalType);
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }

            il.Emit(OpCodes.Ret);
            FastInvokeHandler invoder = (FastInvokeHandler)dynamicMethod.CreateDelegate(typeof(FastInvokeHandler));
            return invoder;
        }

        private static void EmitCastToReference(ILGenerator il, System.Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        private static void EmitBoxIfNeeded(ILGenerator il, System.Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }

    }
}
