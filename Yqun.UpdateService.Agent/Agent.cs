using System;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using WcfExtensions;
using TransferServiceCommon;

namespace Yqun.UpdateService
{
    public class Agent
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
                var proxy = WcfClientHelper.GetProxy<ITransferService>(configFile);

                Hashtable hashtable = new Hashtable();
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
                logger.Error(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
