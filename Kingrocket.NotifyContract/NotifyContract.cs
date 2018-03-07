using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Kingrocket.NotifyContract
{
    [ServiceContract(CallbackContract = typeof(IClient), Namespace = "http://nsmsg.kingrocket.com")]
    public interface IMessageService
    {
        [OperationContract]
        void RegisterClient();

        [OperationContract]
        void NotifyAll(String msg);

        [OperationContract]
        void NotifyUsers(List<string> lstReceiveIDs, String msg);

        [OperationContract]
        void Ping();
    }
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
        //[DataContractFormat]
        //string SessionID{get;set;}
    }
}
