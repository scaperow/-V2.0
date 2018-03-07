using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;
using LABTRANSINTERFACE;
using System.ServiceModel;

namespace UnitTests
{
    public class UploadGGZXTest
    {
        [Test]
        public void HexToFloatTest()
        {
            String zipFile = "d:\\01.zip";
            String addr = "http://125.35.11.31/DotNetFrame/ModuleSources/SYSZX/DataTransport/DataTransport.svc";
            String ksign = "01";
            String JSDWCode = "012701";

            FileStream stream = null;
            stream = new FileInfo(zipFile).OpenRead();
            Byte[] buffer = new Byte[stream.Length];
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            stream.Close();
            String result = UploadToServer(addr, buffer, ksign, JSDWCode);
            Assert.AreEqual("432", result);
        }

        private String UploadToServer(String address, byte[] testData, String ksign, String JSDWCode)
        {
            String result = "";
            try
            {
                using (ChannelFactory<IDataTransport> channelFactory =
                    new ChannelFactory<IDataTransport>("uploadEP", new EndpointAddress(address)))
                {

                    IDataTransport proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        result = proxy.ReciveTestData(testData, ksign, JSDWCode);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
