using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections;
using System.IO;

namespace TransferServiceCommon
{
    [ServiceContract]
    public interface ITransferService
    {
        [OperationContract]
        Stream InvokeMethod(Stream Params);
    }
}
