using System;
using System.Collections.Generic;
using System.Text;
using UpdaterCommon;
using WcfExtensions;
using TransferServiceCommon;

namespace UpdaterComponents
{
    public class ServerFileVersionManager
    {
        public static List<UpdaterFileInfo> GetExistingFile()
        {
            return CallRemoteService("Yqun.BO.ApplicationUpdater.dll", "GetExistingFile", new object[] { }) as List<UpdaterFileInfo>;
        }

        public static Boolean SaveUpdaterFile(List<UpdaterFileInfo> UpdaterFileInfo)
        {
            return Convert.ToBoolean(CallRemoteService("Yqun.BO.ApplicationUpdater.dll", "SaveUpdaterFile", new object[] { UpdaterFileInfo }));
        }

        public static List<UpdaterFileInfo> GetNewUpdaterFile(List<UpdaterFileInfo> UpdaterFileInfo)
        {
            return CallRemoteService("Yqun.BO.ApplicationUpdater.dll", "GetNewUpdaterFile", new object[] { UpdaterFileInfo }) as List<UpdaterFileInfo>;
        }

        public static object CallRemoteService(string AssemblyName, string MethodName, object[] Parameters)
        {
            try
            {
                var configFile = AppDomain.CurrentDomain.BaseDirectory + "\\" +System.Windows.Forms.Application.ProductName + ".exe.config";
                configFile = System.IO.Path.GetFullPath(configFile);

                var proxy = WcfClientHelper.GetProxy<ITransferService>(configFile);
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                hashtable["assembly_name"] = AssemblyName;
                hashtable["method_name"] = MethodName;
                hashtable["method_paremeters"] = Parameters;
                //DateTime time = DateTime.Now;
                //logger.Error("Method CallRemoteServer In");
            System.IO.Stream source_stream = Serialize.SerializeToStream(hashtable);
            System.IO.Stream zip_stream = Compression.CompressStream(source_stream); source_stream.Dispose();
            System.IO.Stream stream_result = proxy.InvokeMethod(zip_stream); zip_stream.Dispose();
            System.IO.Stream ms = ReadMemoryStream(stream_result); stream_result.Dispose();
            System.IO.Stream unzip_stream = Compression.DeCompressStream(ms); ms.Dispose();
            System.Collections.Hashtable Result = Serialize.DeSerializeFromStream(unzip_stream) as System.Collections.Hashtable;
                //logger.Error((DateTime.Now - time).Seconds.ToString() + " cast by CallRemoteServer");
                return Result["return_value"];
            }
            catch (Exception ex)
            {
                return null;
                //throw new System.Exception ServiceAccessException(ex.Message, ex);
            }
        }
        /// <summary>
        /// 从输入流中读出内存流
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        private static System.IO.MemoryStream ReadMemoryStream(System.IO.Stream Params)
        {
            System.IO.MemoryStream serviceStream = new System.IO.MemoryStream();
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

    }

}
