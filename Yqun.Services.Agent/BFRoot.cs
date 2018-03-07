using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using Yqun.Common;
using Yqun.Interfaces;
using System.Reflection.Emit;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using System.Data;
using Yqun.Bases;
using System.IO;

namespace Yqun.Services
{
    /// <summary>
    /// 单机版程序调用入口
    /// </summary>
    public class BFRoot
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //缓存用到的所有类型信息
        static IDictionary<string, Type> typeList = new Dictionary<string, Type>();
        //缓存所有用到的方法信息
        static IDictionary<string, MethodInfo> methodList = new Dictionary<string, MethodInfo>();

        //读取单机版数据库配置信息
        static BFRoot()
        {
        }

        /// <summary>
        /// 设置服务端的数据库连接信息
        /// </summary>
        public static void SettingDBConnectionInfo()
        {
            DataSet DataSet = new DataSet("DataSourceSet");
            String FileName = Path.Combine(SystemFolder.DataSourceFolder, "DataSource.xml");

            if (File.Exists(FileName))
            {
                DataSet.ReadXml(FileName);

                String DataAdapterType = DataSetCoder.GetProperty(DataSet, 0, "name", "DataAdapterType", "value").ToString();
                String DataBaseType = DataSetCoder.GetProperty(DataSet, 0, "name", "DataBaseType", "value").ToString();
                String DataSource = DataSetCoder.GetProperty(DataSet, 0, "name", "DataSource", "value").ToString();
                String DataInstance = DataSetCoder.GetProperty(DataSet, 0, "name", "DataInstance", "value").ToString();
                String DataUserName = DataSetCoder.GetProperty(DataSet, 0, "name", "DataUserName", "value").ToString();
                String DataPassword = DataSetCoder.GetProperty(DataSet, 0, "name", "DataPassword", "value").ToString();
                String DataISAttach = DataSetCoder.GetProperty(DataSet, 0, "name", "DataISAttach", "value").ToString();
                String DataIntegratedLogin = DataSetCoder.GetProperty(DataSet, 0, "name", "DataIntegratedLogin", "value").ToString();

                ServerLoginInfos.DBConnectionInfo.DataAdapterType = DataAdapterType;
                ServerLoginInfos.DBConnectionInfo.DataBaseType = DataBaseType;
                ServerLoginInfos.DBConnectionInfo.DataSource = EncryptSerivce.Dencrypt(DataSource);
                ServerLoginInfos.DBConnectionInfo.DataInstance = EncryptSerivce.Dencrypt(DataInstance);
                ServerLoginInfos.DBConnectionInfo.DataUserName = EncryptSerivce.Dencrypt(DataUserName);
                ServerLoginInfos.DBConnectionInfo.DataPassword = EncryptSerivce.Dencrypt(DataPassword);
                ServerLoginInfos.DBConnectionInfo.DataISAttach = DataISAttach;
                ServerLoginInfos.DBConnectionInfo.LocalStartPath = AppDomain.CurrentDomain.BaseDirectory + @"\";
                ServerLoginInfos.DBConnectionInfo.DataIntegratedLogin = DataIntegratedLogin;
            }
        }
        /// <summary>
        /// 通过参数设置服务端的数据库连接信息
        /// </summary>
        public static void SettingDBConnectionInfoWithArgs(string DataSource, string DataInstance, string DataUserName, string DataPassword)
        {

            String DataAdapterType = "SQLClient";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataAdapterType", "value").ToString();
            String DataBaseType = "MSSQLServer2k5";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataBaseType", "value").ToString();
            String DataISAttach = "False";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataISAttach", "value").ToString();
            String DataIntegratedLogin = "False";// DataSetCoder.GetProperty(DataSet, 0, "name", "DataIntegratedLogin", "value").ToString();

            ServerLoginInfos.DBConnectionInfo.DataAdapterType = DataAdapterType;
            ServerLoginInfos.DBConnectionInfo.DataBaseType = DataBaseType;
            ServerLoginInfos.DBConnectionInfo.DataSource = DataSource;
            ServerLoginInfos.DBConnectionInfo.DataInstance = DataInstance;
            ServerLoginInfos.DBConnectionInfo.DataUserName = DataUserName;
            ServerLoginInfos.DBConnectionInfo.DataPassword = DataPassword;
            ServerLoginInfos.DBConnectionInfo.DataISAttach = DataISAttach;
            ServerLoginInfos.DBConnectionInfo.LocalStartPath = AppDomain.CurrentDomain.BaseDirectory + @"\";
            ServerLoginInfos.DBConnectionInfo.DataIntegratedLogin = DataIntegratedLogin;

        }

        public static Hashtable ExcuteMessage(Hashtable Message)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\".ToCharArray()) + @"\";
                object[] ConnectionArgs = Message["connection_parameters"] as object[] ?? null;
                string Assembly_Name = Message["assembly_name"] as string;
                string FileName = path.Trim() + Assembly_Name.Trim();
                string Method_Name = Message["method_name"] as string;
                Method_Name = Method_Name.Trim();
                object[] Method_Paremeters = (object[])Message["method_paremeters"];

                object o;
                if (ConnectionArgs == null)
                {
                    o = InvokeMethod(FileName, Method_Name, Method_Paremeters);
                }
                else
                {
                    o = InvokeMethod(ConnectionArgs, FileName, Method_Name, Method_Paremeters);
                }

                Hashtable t = new Hashtable();
                t.Add("return_value", o);
                return t;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("访问ExcuteMessage(Hashtable Message)方法出错,原因是{0}", ee.Message));

                Hashtable t = new Hashtable();
                t.Add("return_value", null);
                return t;
            }
        }

        static object InvokeMethod(object[] ConnectionArgs, string FileName, string MethodName, object[] Args)
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
                logger.Error(string.Format("访问方法InvokeMethod(object[] ConnectionArgs, string FileName, string MethodName, object[] Args)出错，原因为“{0}”", ee.Message));
                return null;
            }
        }

        static object InvokeMethod(string FileName, string MethodName, object[] Args)
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
                    obj = Activator.CreateInstance(type, false);
                FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(methodInfo);
                return fastInvoker(obj, Args);
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("访问方法InvokeMethod(string FileName, string MethodName, object[] Args)出错，原因为“{0}”", ee.Message));
                return null;
            }

        }

        static Type FindType(string filename, string methodname, params Type[] paramtypes)
        {
            Assembly a = Assembly.LoadFrom(filename);
            Type[] types = a.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (!types[i].IsPublic) continue;

                MethodInfo methodInfo = types[i].GetMethod(methodname, paramtypes);
                if (methodInfo != null)
                {
                    if (!methodList.ContainsKey(methodname))
                    {
                        methodList.Add(methodname, methodInfo);
                    }

                    return types[i];
                }
            }

            logger.Error(string.Format("FindType(string filename, string methodname, params Type[] paramtypes)，原因是方法 {0} 所属的类型没找到！", methodname));
            return null;
        }

        static Type GetType(string filename, string methodname, params Type[] types)
        {
            Type type = null;
            if (!typeList.ContainsKey(filename + methodname))
            {
                type = FindType(filename, methodname, types);
                typeList.Add(filename + methodname, type);
            }
            else
                type = typeList[filename + methodname];
            return type;
        }

        static MethodInfo GetMethod(string MethodName, Type type, params Type[] types)
        {
            MethodInfo methodInfo = null;
            string MethodKey = MethodName;
            for (int i = 0; i < types.Length; i++)
            {
                MethodKey = MethodKey + "_" + types[i].ToString();
            }

            if (!methodList.ContainsKey(MethodKey))
            {
                methodInfo = type.GetMethod(MethodName, types);
                methodList.Add(MethodKey, methodInfo);
            }
            else
                methodInfo = methodList[MethodKey];
            return methodInfo;
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
