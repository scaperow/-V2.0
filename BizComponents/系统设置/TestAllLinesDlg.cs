using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using BizCommon;
using FarPoint.Win.Spread;
using Yqun.Bases;
using System.IO;
using System.Collections;
using System.ServiceModel;
using TransferServiceCommon;

namespace BizComponents
{
    public partial class TestAllLinesDlg : Form
    {
        public TestAllLinesDlg()
        {
            InitializeComponent();
        }

        void treeModule_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode item in e.Node.Nodes)
                    {
                        item.Checked = e.Node.Checked;
                    }
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            DataTable dt = LineHelperClient.GetLineList();

            if (dt != null)
            {
                txtResult.Text = string.Format("总共需要{0}条线路需要测试\r\n", dt.Rows.Count) + txtResult.Text;
                string LineName, IPAddress, Port, errMsg, strFailLines = string.Empty ;
                int i = 1;
                int iSuccess = 0;
                int iFail = 0;
                lblMsg.Text = "开始测试";
                foreach (DataRow row in dt.Rows)
                {
                    LineName = row["LineName"].ToString();
                    IPAddress = row["IPAddress"].ToString();
                    Port = row["Port"].ToString();
                    lblMsg.Text = LineName + "……";
                    bool bSuccess = TestNetwork(IPAddress, Port, out errMsg);
                    if (bSuccess == true && string.IsNullOrEmpty(errMsg))
                    {
                        iSuccess++;
                        txtResult.Text = string.Format("{0}、【{1}】连接成功\r\n", i, LineName) + txtResult.Text;
                    }
                    else
                    {
                        iFail++;
                        txtResult.Text = string.Format("{0}、【{1}】连接失败，原因：{2}\r\n", i, LineName, errMsg) + txtResult.Text;
                        strFailLines += LineName + ",";
                    }
                    i++;
                }
                lblMsg.Text = "测试结束";
                txtResult.Text = string.Format("所有线路测试结束,成功：{0}  失败:{1}  失败线路：{2}\r\n", iSuccess, iFail,strFailLines) + txtResult.Text;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
        }
        #region 通用方法

        private bool TestNetwork(string IPAddress, string Port, out string errMsg)
        {
            bool bSuccess = false;
            try
            {
                String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
                bSuccess = Convert.ToBoolean(CallRemoteServerMethod(lineAddress, "Yqun.BO.LoginBO.dll", "TestNetwork",
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
    }
}
