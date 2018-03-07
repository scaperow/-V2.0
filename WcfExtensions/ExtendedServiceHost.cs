/**
 * File name: ExtendedServiceHost.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/3/2009 12:47:44 PM format: MM/dd/yyyy
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
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class ExtendedServiceHost:ServiceHost
    {
        #region Member Variables

        #endregion

        #region Constructors

        public ExtendedServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        public ExtendedServiceHost(Type serviceType)
            : base(serviceType)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        //Overriding ApplyConfiguration() allows us to 
        //alter the ServiceDescription prior to opening
        //the service host. 
        protected override void ApplyConfiguration()
        {
            try
            {
                //First, we call base.ApplyConfiguration()
                //to read any configuration that was provided for
                //the service we're hosting. After this call,
                //this.ServiceDescription describes the service
                //as it was configured.
                base.ApplyConfiguration();

                //Now that we've populated the ServiceDescription, we can reach into it
                //and do interesting things (in this case, we'll add an instance of
                //ServiceMetadataBehavior if it's not already there.
                var mexBehavior = Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (mexBehavior == null)
                {
                    mexBehavior = new ServiceMetadataBehavior();
                    Description.Behaviors.Add(mexBehavior);
                }
                else
                {
                    //Metadata behavior has already been configured, 
                    //so we don't have any work to do.
                    return;
                }

                //Add a metadata endpoint at each base address
                //using the "/mex" addressing convention
                foreach (var baseAddress in BaseAddresses)
                {
                    if (baseAddress.Scheme == Uri.UriSchemeHttp)
                    {
                        mexBehavior.HttpGetEnabled = true;
                        AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                           MetadataExchangeBindings.CreateMexHttpBinding(),
                                           "mex");
                    }
                    else if (baseAddress.Scheme == Uri.UriSchemeHttps)
                    {
                        mexBehavior.HttpsGetEnabled = true;
                        AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                           MetadataExchangeBindings.CreateMexHttpsBinding(),
                                           "mex");
                    }
                    else if (baseAddress.Scheme == Uri.UriSchemeNetPipe)
                    {
                        AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                           MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                           "mex");
                    }
                    else if (baseAddress.Scheme == Uri.UriSchemeNetTcp)
                    {
                        AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                           MetadataExchangeBindings.CreateMexTcpBinding(),
                                           "mex");
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion

    }
}

// end of namespace
