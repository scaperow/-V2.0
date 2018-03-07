/**
 * File name: ExtendedServiceBehavior.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/3/2009 4:48:42 PM format: MM/dd/yyyy
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
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class ExtendedServiceBehavior:Attribute, IServiceBehavior, IEndpointBehavior, IContractBehavior
    {
        #region Methods

        private void ApplyDispatchBehavior(ChannelDispatcher dispatcher)
        {
            // Don't add an error handler if it already exists
            foreach (IErrorHandler errorHandler in dispatcher.ErrorHandlers)
            {
                if (errorHandler is ExtendedServiceErrorHandler)
                {
                    return;
                }
            }
            dispatcher.ErrorHandlers.Add(new ExtendedServiceErrorHandler());
        }


        private void ApplyClientBehavior(ClientRuntime runtime)
        {
            // Don't add a message inspector if it already exists
            foreach (IClientMessageInspector messageInspector in runtime.MessageInspectors)
            {
                if (messageInspector is ExtendedClientMessageInspector)
                {
                    return;
                }
            }

            runtime.MessageInspectors.Add(new ExtendedClientMessageInspector());
        }

        #endregion

        #region IServiceBehavior Members

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return;
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                ApplyDispatchBehavior(dispatcher);

                foreach (EndpointDispatcher endpointDispatcher in dispatcher.Endpoints)
                {
                    foreach (DispatchOperation operation in endpointDispatcher.DispatchRuntime.Operations)
                    {
                        operation.CallContextInitializers.Add(new ContextReceivalCallContextInitializer());
                    }
                }
            }
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return;
        }

        #endregion

        #region IEndpointBehavior Members

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ApplyClientBehavior(clientRuntime);

            clientRuntime.MessageInspectors.Add(new ContextSendInspector());
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            ApplyDispatchBehavior(endpointDispatcher.ChannelDispatcher);

            foreach (DispatchOperation operation in endpointDispatcher.DispatchRuntime.Operations)
            {
                operation.CallContextInitializers.Add(new ContextReceivalCallContextInitializer());
            }
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
            return;
        }

        #endregion

        #region IContractBehavior Members

        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ApplyClientBehavior(clientRuntime);
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            ApplyDispatchBehavior(dispatchRuntime.ChannelDispatcher);
        }

        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            return;
        }

        #endregion
    }
}

// end of namespace
