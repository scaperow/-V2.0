using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Yqun.Common.ContextCache;

namespace WcfExtensions
{
    public class ContextReceivalCallContextInitializer : ICallContextInitializer
    {
        public void AfterInvoke(object correlationState)
        {
            ApplicationContext.Current.Clear();
        }
        
        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            if (message.Headers.FindHeader(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace) != -1)
            {
                ApplicationContext.Current = message.Headers.GetHeader<ApplicationContext>(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextHeaderNamespace);
            }

            return null;
        }
    }
}
