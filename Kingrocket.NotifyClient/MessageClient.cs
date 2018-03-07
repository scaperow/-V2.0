using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingrocket.NotifyContract;

namespace Kingrocket.NotifyClient
{
    public delegate void ReceiveMessageDeleage(string message);

    public class MessageClient : IClient
    {
        public ReceiveMessageDeleage ReceiveMessage;
        //private string sessionID;
        //public string SessionID  // read-write instance property
        //{
        //    get
        //    {
        //        return sessionID;
        //    }
        //    set
        //    {
        //        sessionID = value;
        //    }
        //}
        public void SendMessage(string message)
        {
            this.ReceiveMessage(message);
        }
    }  
}
