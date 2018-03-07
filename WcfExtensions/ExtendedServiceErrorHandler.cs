/**
 * File name: ExtendedServiceErrorHandler.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/3/2009 7:07:37 PM format: MM/dd/yyyy
 * 
 * 
 * Modification history:
 * Name				Date					Desc
 * 
 *  
 * Version: 1.0
 * */

#region Using Directives

using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class ExtendedServiceErrorHandler:IErrorHandler
    {
        #region IErrorHandler Members

        bool IErrorHandler.HandleError(Exception error)
        {
            if (error is FaultException)
            {
                return false; // Let WCF do normal processing
            }
            return true; // Fault message is already generated
        }

        void IErrorHandler.ProvideFault(Exception error,
                                        MessageVersion version,
                                        ref Message fault)
        {
            if (error is FaultException)
            {
                // Let WCF do normal processing
            }
            else
            {
                // Generate fault message manually
                MessageFault messageFault = MessageFault.CreateFault(
                    new FaultCode("Sender"),
                    new FaultReason(error.Message),
                    error,
                    new NetDataContractSerializer());
                fault = Message.CreateMessage(version, messageFault, null);
            }
        }

        #endregion
    }
}

// end of namespace
