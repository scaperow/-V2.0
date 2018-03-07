using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using Yqun.Common.ContextCache;
using System.ServiceModel;

namespace WcfExtensions
{
    public  class ContextSendInspector : IClientMessageInspector
    {
        #region IClientMessageInspector 成员

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {}

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            request.Headers.Add(new MessageHeader<ApplicationContext>(
                                ApplicationContext.Current).GetUntypedHeader(
                                ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace));

            return null;
        }

        #endregion
    }
}
