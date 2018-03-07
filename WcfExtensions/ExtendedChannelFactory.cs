/**
 * File name: ExtendedChannelFactory.cs 
 * Author: Mosfiqur.Rahman
 * Date: 7/21/2009 5:05:58 PM format: MM/dd/yyyy
 * 
 * 
 * Modification history:
 * Name				Date					Desc
 * 
 *  
 * Version: 1.0
 * */

using System;
using System.Configuration;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace WcfExtensions
{
    /// <summary>
    /// A custom channel factory which takes in a path to a
    /// custom configuration file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtendedChannelFactory<T> : ChannelFactory<T>
    {
        readonly string _configurationPath;

        /// <summary>
        /// custom client channel constructor which
        /// specifies an external configuration file
        /// </summary>
        /// <param name="configurationPath"></param>
        public ExtendedChannelFactory(string configurationPath)
            : base(typeof(T))
        {
            _configurationPath = configurationPath;
            InitializeEndpoint((string)null, null);
        }

        /// <summary>
        /// overrides the CreateDescription() method of the channel factory
        /// to apply a new configuration file
        /// </summary>
        /// <returns></returns>
        protected override ServiceEndpoint CreateDescription()
        {
            var serviceEndpoint = base.CreateDescription();

            var executionFileMap = new ExeConfigurationFileMap {ExeConfigFilename = _configurationPath};

            var config = ConfigurationManager.OpenMappedExeConfiguration(executionFileMap, ConfigurationUserLevel.None);
            var serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(config);

            ChannelEndpointElement selectedEndpoint = null;

            foreach (ChannelEndpointElement endpoint in serviceModelSectionGroup.Client.Endpoints)
            {
                if (endpoint.Contract == serviceEndpoint.Contract.ConfigurationName)
                {
                    selectedEndpoint = endpoint;
                    break;
                }
            }

            if (selectedEndpoint != null)
            {
                if (serviceEndpoint.Binding == null)
                {
                    serviceEndpoint.Binding = CreateBinding(selectedEndpoint.Binding, serviceModelSectionGroup);
                }

                if (serviceEndpoint.Address == null)
                {
                    serviceEndpoint.Address = new EndpointAddress(selectedEndpoint.Address, GetIdentity(selectedEndpoint.Identity), selectedEndpoint.Headers.Headers);
                }

                if (serviceEndpoint.Behaviors.Count == 0 && !String.IsNullOrEmpty(selectedEndpoint.BehaviorConfiguration))
                {
                    AddBehaviors(selectedEndpoint.BehaviorConfiguration, serviceEndpoint, serviceModelSectionGroup);
                }

                serviceEndpoint.Name = selectedEndpoint.Contract;
            }

            return serviceEndpoint;
        }

        /// <summary>
        /// Configures the binding for the selected endpoint
        /// </summary>
        /// <param name="bindingName"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private Binding CreateBinding(string bindingName, ServiceModelSectionGroup group)
        {
            BindingCollectionElement bindingElementCollection = group.Bindings[bindingName];
            if (bindingElementCollection.ConfiguredBindings.Count > 0)
            {
                IBindingConfigurationElement be = bindingElementCollection.ConfiguredBindings[0];

                Binding binding = GetBinding(be);
                if (be != null)
                {
                    be.ApplyConfiguration(binding);
                }

                return binding;
            }

            return null;
        }

        /// <summary>
        /// Adds the configured behavior to the selected endpoint
        /// </summary>
        /// <param name="behaviorConfiguration"></param>
        /// <param name="serviceEndpoint"></param>
        /// <param name="group"></param>
        private void AddBehaviors(string behaviorConfiguration, ServiceEndpoint serviceEndpoint, ServiceModelSectionGroup group)
        {
            var behaviorElement = group.Behaviors.EndpointBehaviors[behaviorConfiguration];
            for (int i = 0; i < behaviorElement.Count; i++)
            {
                BehaviorExtensionElement behaviorExtension = behaviorElement[i];
                var extension = behaviorExtension.GetType().InvokeMember("CreateBehavior",
                                                                            BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                                                                            null, behaviorExtension, null);
                if (extension != null)
                {
                    serviceEndpoint.Behaviors.Add((IEndpointBehavior)extension);
                }
            }
        }

        /// <summary>
        /// Gets the endpoint identity from the configuration file
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private EndpointIdentity GetIdentity(IdentityElement element)
        {
            EndpointIdentity identity = null;
            var properties = element.ElementInformation.Properties;
            if (properties["userPrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateUpnIdentity(element.UserPrincipalName.Value);
            }
            if (properties["servicePrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateSpnIdentity(element.ServicePrincipalName.Value);
            }
            if (properties["dns"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateDnsIdentity(element.Dns.Value);
            }
            if (properties["rsa"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateRsaIdentity(element.Rsa.Value);
            }
            if (properties["certificate"].ValueOrigin != PropertyValueOrigin.Default)
            {
                var supportingCertificates = new X509Certificate2Collection();
                supportingCertificates.Import(Convert.FromBase64String(element.Certificate.EncodedValue));
                if (supportingCertificates.Count == 0)
                {
                    throw new InvalidOperationException("UnableToLoadCertificateIdentity");
                }
                X509Certificate2 primaryCertificate = supportingCertificates[0];
                supportingCertificates.RemoveAt(0);
                return EndpointIdentity.CreateX509CertificateIdentity(primaryCertificate, supportingCertificates);
            }

            return identity;
        }

        /// <summary>
        /// Helper method to create the right binding depending on the configuration element
        /// </summary>
        /// <param name="configurationElement"></param>
        /// <returns></returns>
        private Binding GetBinding(IBindingConfigurationElement configurationElement)
        {
            if (configurationElement is CustomBindingElement)
                return new CustomBinding();
            if (configurationElement is BasicHttpBindingElement)
                return new BasicHttpBinding();
            if (configurationElement is NetMsmqBindingElement)
                return new NetMsmqBinding();
            if (configurationElement is NetNamedPipeBindingElement)
                return new NetNamedPipeBinding();
            if (configurationElement is NetPeerTcpBindingElement)
                return new NetPeerTcpBinding();
            if (configurationElement is NetTcpBindingElement)
                return new NetTcpBinding();
            if (configurationElement is WSDualHttpBindingElement)
                return new WSDualHttpBinding();
            if (configurationElement is WSHttpBindingElement)
                return new WSHttpBinding();
            if (configurationElement is WSFederationHttpBindingElement)
                return new WSFederationHttpBinding();

            return null;
        }
    }
}