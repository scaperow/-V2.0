using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Yqun.BO.ReminderManager
{
    public class ServiceBroker
    {
        public static T FindService<T>()
        {
            ChannelFactory<T> chanel = new ChannelFactory<T>(typeof(T).Name);
            return chanel.CreateChannel();
        }

        public static T FindService<T>(string endPointName)
        {
            ChannelFactory<T> chanel = new ChannelFactory<T>(endPointName);
            return chanel.CreateChannel();
        }

        public static void DisposeService<T>(T channel) where T : class
        {
            ICommunicationObject obj2 = channel as ICommunicationObject;
            if ((obj2 != null) && (obj2.State != CommunicationState.Closed))
            {
                try
                {
                    if (obj2.State != CommunicationState.Faulted)
                    {
                        obj2.Close();
                    }
                    else
                    {
                        obj2.Abort();
                    }
                }
                catch
                { }
            }
        }
    }
}
