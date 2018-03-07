using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace Kingrocket.NotifyService
{
    
    public partial class NotifyService : ServiceBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("loggerAX");

        private ServiceHost _host = null;

        public NotifyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            _host = new ServiceHost(typeof(MessageService));
            _host.Open();
            logger.Error("open");
            MessageService.OnClientRegister = (SessionID, LineID, UserName, LineName, TestRoomCode, SegmentName, CompanyName, TestRoomName) =>
            {
                SQLHelper.RegisterOnlineUser(SessionID, LineID, UserName, LineName, TestRoomCode, SegmentName, CompanyName, TestRoomName);
                logger.Error("OnClientRegister " + OperationContext.Current.SessionId + " " + LineID + " " + UserName);
                //logger.Error(id + " registered");
            };

            MessageService.OnClientClosed = (SessionID) =>
            {
                logger.Error(SessionID + " closed");
                SQLHelper.DeleteOnlineUser(SessionID);
            };
            
        }


        protected override void OnStop()
        {
            if (_host != null)
            {
                _host.Close();
            }
            logger.Error("close");
        }
    }
}
