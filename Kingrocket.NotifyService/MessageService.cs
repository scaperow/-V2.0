using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Collections;
using Kingrocket.NotifyContract;

namespace Kingrocket.NotifyService
{
    public delegate void ClientRegesterDelegate(string SessionID, string LineID, string UserName, string LineName, string TestRoomCode, string SegmentName, string CompanyName, string TestRoomName);
    public delegate void ClientClosedDelegate(string SessionID);

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : IMessageService, IDisposable
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("loggerAX");

        public static List<IClient> ClientCallbackList { get; set; }

        public static ClientRegesterDelegate OnClientRegister;
        public static ClientClosedDelegate OnClientClosed;
        public static readonly Hashtable clients = new Hashtable();

        public MessageService()
        {
            ClientCallbackList = new List<IClient>();
        }

        public void RegisterClient()
        {
            var client = OperationContext.Current.GetCallbackChannel<IClient>();
            //var id = OperationContext.Current.SessionId;
            var ns = "http://nsmsg.kingrocket.com";
            string SessionID = OperationContext.Current.SessionId.ToLower();
            var LineID = GetHeaderValue("LineID", ns);
            var UserName = GetHeaderValue("UserName", ns);
            var LineName = GetHeaderValue("LineName", ns);
            var TestRoomCode = GetHeaderValue("TestRoomCode", ns);
            var SegmentName = GetHeaderValue("SegmentName", ns);
            var CompanyName = GetHeaderValue("CompanyName", ns);
            var TestRoomName = GetHeaderValue("TestRoomName", ns);
            //client.SessionID = SessionID;
            clients[SessionID] = client;
            OnClientRegister(SessionID, LineID, UserName, LineName, TestRoomCode, SegmentName, CompanyName, TestRoomName);
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
            ClientCallbackList.Add(client);
        }

        public void Ping()
        {
            try
            {
                var id = OperationContext.Current.SessionId;
                var ns = "http://nsmsg.kingrocket.com";
                string SessionID = OperationContext.Current.SessionId;
                SQLHelper.UpdateLastActiveTime(SessionID);
            }
            catch (Exception ex)
            {
                logger.Error("Ping error:" + ex.Message);
            }
        }

        private string GetHeaderValue(string name, string ns)
        {
            var headers = OperationContext.Current.IncomingMessageHeaders;
            var index = headers.FindHeader(name, ns);
            if (index > -1)
                return headers.GetHeader<string>(index);
            else
                return "";
        }

        void Channel_Closing(object sender, EventArgs e)
        {
            lock (ClientCallbackList)
            {
                try
                {
                    var client = (IClient)sender;
                    string id = string.Empty; ;
                    foreach (DictionaryEntry de in clients) 
                    {
                        if ((IClient)de.Value == client)
                        {
                            id = de.Key.ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(id))
                    {
                        OnClientClosed(id.ToString());
                    }
                    ClientCallbackList.Remove((IClient)sender);
                }
                catch (Exception ex)
                {
                    logger.Error("Channel_Closing error:" + ex.Message);
                }
            }
        }
        public void Dispose()
        {
            ClientCallbackList.Clear();
        }

        public void NotifyAll(string msg)
        {
            if (ClientCallbackList == null || ClientCallbackList.Count == 0)
                return;
            lock (ClientCallbackList)
            {
                Kingrocket.NotifyService.OnlineUserInfo mdlUser;
                foreach (var client in ClientCallbackList)
                {
                    var id = clients[client].ToString();
                    logger.Error("NotifyAll clients[client]:" + id);
                    if (id.ToLower() != OperationContext.Current.SessionId.ToLower())
                    {
                        mdlUser = SQLHelper.GetOnlineUserInfo(id);
                        if (mdlUser != null && mdlUser.LineID == new Guid("7155B19C-CC69-460F-B8AC-C4E773517AFE"))
                        {
                            logger.Error("NotifyAll SessionID:" + mdlUser.SessionID + " UserName:" + mdlUser.UserName + " msg:" + msg);
                            client.SendMessage(msg);
                        }
                        else
                        {
                            logger.Error("NotifyAll SessionID:" + id + " is null ");
                        }
                    }
                }
            }
        }

        public void NotifyUsers(List<string> lstReceiveIDs, string msg)
        {
            if (ClientCallbackList == null || ClientCallbackList.Count == 0)
                return;
            lock (ClientCallbackList)
            {
                Kingrocket.NotifyService.OnlineUserInfo mdlUser;
                foreach (var id in lstReceiveIDs)
                {
                    if (id.ToLower() != OperationContext.Current.SessionId.ToLower())
                    {
                        var client = (IClient)clients[id.ToLower()];

                        mdlUser = SQLHelper.GetOnlineUserInfo(id);
                        if (mdlUser != null)
                        {
                            logger.Error("NotifyUsers SessionID:" + mdlUser.SessionID + " UserName:" + mdlUser.UserName + " msg:" + msg);
                            client.SendMessage(msg);
                        }
                        else
                        {
                            logger.Error("NotifyUsers SessionID:" + id + " is null ");
                        }
                    }
                }
                //foreach (var client in ClientCallbackList)
                //{
                //    var id = clients[client].ToString();
                //    logger.Error("NotifyAll clients[client]:" + id);
                //    if (id.ToLower() != OperationContext.Current.SessionId.ToLower())
                //    {
                //        mdlUser = SQLHelper.GetOnlineUserInfo(id);
                //        if (mdlUser != null && mdlUser.LineID == new Guid("7155B19C-CC69-460F-B8AC-C4E773517AFE"))
                //        {
                //            logger.Error("NotifyAll SessionID:" + mdlUser.SessionID + " UserName:" + mdlUser.UserName + " msg:" + msg);
                //            client.SendMessage(msg);
                //        }
                //        else
                //        {
                //            logger.Error("NotifyAll SessionID:" + id + " is null ");
                //        }
                //    }
                //}
            }
        }

    }
}
