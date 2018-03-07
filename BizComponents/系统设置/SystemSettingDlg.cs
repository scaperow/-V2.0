using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BizCommon;
using System.ServiceModel;
using TransferServiceCommon;
using System.Collections;

namespace BizComponents
{
    public partial class SystemSettingDlg : Form
    {
        public SystemSettingDlg()
        {
            InitializeComponent();
        }

        private void btn_uploadGL_Click(object sender, EventArgs e)
        {
            SaveUpdateZipFile("1");
        }

        private void btn_uploadCJ_Click(object sender, EventArgs e)
        {
            SaveUpdateZipFile("2");
        }

        private void SaveUpdateZipFile(String flag)
        {
            //上传更新包
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "压缩文件(*.zip)|*.zip";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.FileName != "")
                {
                    FileStream stream = null;
                    JZFile f = new JZFile();
                    f.FileName = Path.GetFileName(dlg.FileName);
                    stream = new FileInfo(dlg.FileName).OpenRead();
                    Byte[] buffer = new Byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    f.FileType = flag;
                    f.FileData = buffer;
                    stream.Close();
                    Boolean result = UploadHelperClient.UploadUpdateFile(f);
                    MessageBox.Show(result ? "成功" : "失败");
                }
            }
        }

        private void btn_uploadDY_Click(object sender, EventArgs e)
        {
            SaveUpdateZipFile("9");
        }

        private void bt_uploadGL_All_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "压缩文件(*.zip)|*.zip";
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.FileName != "")
                {
                    String errorLineName = "";
                    FileStream stream = null;
                    JZFile f = new JZFile();
                    f.FileName = Path.GetFileName(dlg.FileName);
                    stream = new FileInfo(dlg.FileName).OpenRead();
                    Byte[] buffer = new Byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    f.FileType = "1";
                    f.FileData = buffer;
                    stream.Close();
                    DataTable dt = LineHelperClient.GetLineList();
                    String msg = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Boolean result = UploadHelperClient.UploadFileByLineID(f, dt.Rows[i]["ID"].ToString());

                            if (!result)
                            {
                                errorLineName += "，" + dt.Rows[i]["LineName"].ToString();
                            }
                        }
                        if (errorLineName != "")
                        {
                            msg = "发布失败，失败线路有" + errorLineName;
                        }
                        else
                        {
                            msg = "全部成功";
                        }
                        MessageBox.Show(msg);
                    }
                }
            }
        }

        private void bt_LD_Click(object sender, EventArgs e)
        {
            SaveUpdateZipFile("10");
        }

        private void btnSyncGGUsers_Click(object sender, EventArgs e)
        {            
            bool bSuccess= ProjectHelperClient.SyncGGCUserAuth();
            if (bSuccess == true)
            {
                MessageBox.Show("同步成功");
            }
            else
            {
                MessageBox.Show("同步失败");
            }
        }

        private void btnTestAllLines_Click(object sender, EventArgs e)
        {
            TestAllLinesDlg dlg = new TestAllLinesDlg();
            dlg.ShowDialog();
        }

        private void btnStartMQ_Click(object sender, EventArgs e)
        {
            List<Sys_Line> lstLine;
            string strLocalServerIP = "112.124.99.146";
            String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
            string errMsg;
            object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetMQWSLinesByIP",
                new Object[] { strLocalServerIP });
            if (objLib != null)
            {
                lstLine = objLib as List<Sys_Line>;
                foreach (Sys_Line line in lstLine)
                {
                    StartMQ(line.LineIP, line.LinePort, out errMsg);
                }
                //string LineName, IPAddress, Port, errMsg, strFailLines = string.Empty;
                //foreach (DataRow row in dt.Rows)
                //{
                //    LineName = row["LineName"].ToString();
                //    IPAddress = row["IPAddress"].ToString();
                //    Port = row["Port"].ToString();
                //    if (IPAddress == "112.124.99.146")
                //    {
                //        StartMQ(IPAddress, Port, out errMsg);
                //    }
                //}
                MessageBox.Show("重启完成");
            }
        }
        #region 通用方法

        private bool StartMQ(string IPAddress, string Port, out string errMsg)
        {
            bool bSuccess = false;
            try
            {
                String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
                bSuccess = Convert.ToBoolean(CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "ManualUploadMQ",
                   new Object[] { }).ToString());
                errMsg = "";
            }
            catch (Exception ex)
            {
                bSuccess = false;
                errMsg = ex.Message;
            }
            return bSuccess;
        }
        public object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
        {
            object obj = null;
            using (ChannelFactory<ITransferService> channelFactory = new ChannelFactory<ITransferService>("TransferServiceEndPoint", new EndpointAddress(address)))
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
        #endregion

        private void btnNotifyAll_Click(object sender, EventArgs e)
        {
            NotifyUserMsgDlg dlg = new NotifyUserMsgDlg();
            dlg.ShowDialog();
        }
    }
}
