/**
 * File name: ServiceBehaviorExtension.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/3/2009 7:22:07 PM format: MM/dd/yyyy
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
using System.ServiceModel.Configuration;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class ServiceBehaviorExtension : BehaviorExtensionElement
    {
        #region Overriden Methods

        public override Type BehaviorType
        {
            get
            {
                return typeof(ExtendedServiceBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new ExtendedServiceBehavior();
        }

        #endregion
    }
}

// end of namespace
