/**
 * File name: ExtendedServiceHostFactory.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/3/2009 1:27:33 PM format: MM/dd/yyyy
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
using System.ServiceModel.Activation;
using System.ServiceModel;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class ExtendedServiceHostFactory:ServiceHostFactory
    {
        #region Methods

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new ServiceHost(serviceType, baseAddresses);
        }

        #endregion
    }
}

// end of namespace
