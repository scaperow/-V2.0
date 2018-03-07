using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
using Kingrocket.NotifyContract;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Kingrocket.NotifyClient
{
    public class ClientHelper
    {
        IMessageService svc;
        DuplexChannelFactory<IMessageService> channelFactory;
        private System.Threading.Timer timer;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("loggerAX");
        //Thread ServiceThread = null;

        public ClientHelper(string LineID, string UserName, string LineName, string TestRoomCode, string SegmentName, string CompanyName, string TestRoomName)
        {
            ThreadParameter parameter = new ThreadParameter();
            parameter.UserName = UserName;
            parameter.LineID = LineID;
            parameter.LineName = LineName;
            parameter.TestRoomCode = TestRoomCode;
            parameter.SegmentName = SegmentName;
            parameter.CompanyName = CompanyName;
            parameter.TestRoomName = TestRoomName;
            ThreadPool.QueueUserWorkItem(new WaitCallback(RegesterClient), parameter);
        }

        private class ThreadParameter
        {
            public string LineID;
            public string UserName;
            public string LineName;
            public string TestRoomCode;
            public string SegmentName;
            public string CompanyName;
            public string TestRoomName;

        }

        private void RegesterClient(object paremeter)
        {
            ThreadParameter tp = paremeter as ThreadParameter;
            var client = new MessageClient();
            client.ReceiveMessage = OnReceiveMessage;
            InstanceContext instanceContext = new InstanceContext(client);
            NetTcpBinding ntb = new NetTcpBinding(SecurityMode.None);
            channelFactory = new DuplexChannelFactory<IMessageService>(
                instanceContext, ntb, "net.tcp://nsmsg.kingrocket.com:9999/KingrocketMessageService/");
            //instanceContext, ntb, "net.tcp://115.29.206.137:9999/KingrocketMessageService/");//115.29.206.137

            svc = channelFactory.CreateChannel();
            try
            {
                using (var scope = new OperationContextScope(svc as IContextChannel))
                {
                    var myNamespace = "http://nsmsg.kingrocket.com";
                    // 注意Header的名字中不能出现空格，因为要作为Xml节点名。  
                    var MHLineID = MessageHeader.CreateHeader("LineID", myNamespace, tp.LineID);
                    var MHUserName = MessageHeader.CreateHeader("UserName", myNamespace, tp.UserName);
                    var MHLineName = MessageHeader.CreateHeader("LineName", myNamespace, tp.LineName);
                    var MHTestRoomCode = MessageHeader.CreateHeader("TestRoomCode", myNamespace, tp.TestRoomCode);
                    var MHSegmentName = MessageHeader.CreateHeader("SegmentName", myNamespace, tp.SegmentName);
                    var MHCompanyName = MessageHeader.CreateHeader("CompanyName", myNamespace, tp.CompanyName);
                    var MHTestRoomName = MessageHeader.CreateHeader("TestRoomName", myNamespace, tp.TestRoomName);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHLineID);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHUserName);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHLineName);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHTestRoomCode);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHSegmentName);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHCompanyName);
                    OperationContext.Current.OutgoingMessageHeaders.Add(MHTestRoomName);

                    svc.RegisterClient();
                    timer = new System.Threading.Timer(p =>
                    {
                        try
                        {
                            svc.Ping();
                        }
                        catch (Exception exx)
                        {
                            logger.Error("timer error:" + exx.Message);
                        }
                    }, null, 1000 * 60 * 5, 1000 * 60 * 5);
                }
            }
            catch (Exception ex)
            {
                logger.Error("ClientHelper error:" + ex.Message);
            }
        }

        void OnReceiveMessage(string message)
        {
            //ThreadMsgParameter parameter = new ThreadMsgParameter();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ShowMessage), parameter);
            //parameter.Message = message;
            if (message.StartsWith("msg:"))
            {//消息
                message = message.Substring(4, message.Length - 4);
                MsgShowFrm frmShow = new MsgShowFrm(message);//MsgShowFrm为要弹出的窗体（提示框）
                frmShow.ScrollShow();
            }
            else if (message.StartsWith("cmd:"))
            {

            }
        }
        private class ThreadMsgParameter
        {
            public string Message;
        }
        private void ShowMessage(object paremeter)
        {
            ThreadMsgParameter tp = paremeter as ThreadMsgParameter;
            MsgShowFrm frmShow = new MsgShowFrm(tp.Message);//Form1为要弹出的窗体（提示框）
            frmShow.ScrollShow();
        }

        public void Close()
        {
            var obj = svc as IDisposable;
            if (obj != null)
            {
                obj.Dispose();
            }
            channelFactory.Close();
            //if (ServiceThread != null)
            //    ServiceThread.Abort();
        }
    }
}
