using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;
using System.Web;
using Yqun.Interfaces;
using Yqun.Bases.Exceptions;
using TransferServiceCommon;
using WcfExtensions;
using System.Windows.Forms;
using Yqun.Common.ContextCache;
using System.Diagnostics;
using Yqun.Common;

namespace Yqun.Services
{
    public class Transfer
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 从输入流中读出内存流
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        private static MemoryStream ReadMemoryStream(Stream Params)
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

        public static object CallRemoteService(string AssemblyName, string MethodName, object[] Parameters)
        {
            try
            {
                var configFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + Application.ProductName + ".exe.config";
                configFile = Path.GetFullPath(configFile);
                Hashtable Result = null;
                //var proxy = WcfClientHelper.GetProxy<ITransferService>(configFile);
                using (ExtendedChannelFactory<ITransferService> channelFactory =
                    new ExtendedChannelFactory<ITransferService>(configFile))
                {
                    channelFactory.Open();
                    ITransferService proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable["assembly_name"] = AssemblyName;
                        hashtable["method_name"] = MethodName;
                        hashtable["method_paremeters"] = Parameters;

                        Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                        Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream); source_stream.Dispose();
                        Stream stream_result = proxy.InvokeMethod(zip_stream); zip_stream.Dispose();
                        Stream ms = ReadMemoryStream(stream_result); stream_result.Dispose();
                        Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms); ms.Dispose();
                        Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;
                    }
                    channelFactory.Close();
                }
                return Result["return_value"];
            }
            catch (Exception ex)
            {
                logger.Error(MethodName + "  " + ex.ToString());
                throw new ServiceAccessException(ex.Message, ex);
            }
        }

        public static object CallLocalService(string AssemblyName, string MethodName, object[] Parameters)
        {
            BFRoot.SettingDBConnectionInfo();

            Hashtable message = new Hashtable();
            message.Add("assembly_name", AssemblyName);
            message.Add("method_name", MethodName);
            message.Add("method_paremeters", Parameters);

            Hashtable message1 = BFRoot.ExcuteMessage(message);
            return message1["return_value"];
        }
        public static object CallLocalServiceWithDBArgs(string AssemblyName, string MethodName, object[] Parameters, string DataSource, string DataInstance, string DataUserName, string DataPassword)
        {
            BFRoot.SettingDBConnectionInfoWithArgs(DataSource, DataInstance, DataUserName, DataPassword);

            Hashtable message = new Hashtable();
            message.Add("assembly_name", AssemblyName);
            message.Add("method_name", MethodName);
            message.Add("method_paremeters", Parameters);

            Hashtable message1 = BFRoot.ExcuteMessage(message);
            return message1["return_value"];
        }

        public static object CallService(string AssemblyName, string MethodName, object[] Parameters)
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService)
            {
                return CallLocalService(AssemblyName, MethodName, Parameters);
            }
            else
            {
                return CallRemoteService(AssemblyName, MethodName, Parameters);
            }
        }

        public static object CallRemoteService(object[] ConnectionArgs, string AssemblyName, string MethodName, object[] Parameters)
        {
            try
            {
                var configFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + Application.ProductName + ".exe.config";
                configFile = Path.GetFullPath(configFile);
                var proxy = WcfClientHelper.GetProxy<ITransferService>(configFile);

                Hashtable hashtable = new Hashtable();
                hashtable["connection_parameters"] = ConnectionArgs;
                hashtable["assembly_name"] = AssemblyName;
                hashtable["method_name"] = MethodName;
                hashtable["method_paremeters"] = Parameters;

                Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream);
                Stream stream_result = proxy.InvokeMethod(zip_stream);
                Stream ms = ReadMemoryStream(stream_result);
                Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                Hashtable Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;
                return Result["return_value"];
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("CallRemoteService(object[] ConnectionArgs, string AssemblyName, string MethodName, object[] Parameters),原因是{0}", ex.Message));
                return null;
            }
        }

        public static object CallLocalService(object[] ConnectionArgs, string AssemblyName, string MethodName, object[] Parameters)
        {
            Hashtable message = new Hashtable();
            message.Add("connection_parameters", ConnectionArgs);
            message.Add("assembly_name", AssemblyName);
            message.Add("method_name", MethodName);
            message.Add("method_paremeters", Parameters);

            Hashtable message1 = BFRoot.ExcuteMessage(message);
            return message1["return_value"];
        }
    }
}
