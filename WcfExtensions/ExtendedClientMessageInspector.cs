/**
 * File name: ExtendedClientMessageInspector.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/4/2009 1:05:34 PM format: MM/dd/yyyy
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
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Xml;
using Yqun.Common.ContextCache;
using System.ServiceModel;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class ExtendedClientMessageInspector:IClientMessageInspector
    {
        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Reads the soap message to find the <soap:Detail> 
        /// node. If found than deserialize it using the 
        /// <see cref="System.Runtime.Serialization.NetDataContractSerializer"/> 
        /// NetDataContractSerializer to construct the exception.
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        private static object ReadFaultDetail(Message reply)
        {
            const string detailElementName = "Detail";

            using (var reader = reply.GetReaderAtBodyContents())
            {
                // Find <soap:Detail>
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName == detailElementName)
                    {
                        break;
                    }
                }

                // Did we find it?
                if (reader.NodeType != XmlNodeType.Element || reader.LocalName != detailElementName)
                {
                    return null;
                }

                // Move to the contents of <soap:Detail>
                if (!reader.Read())
                {
                    return null;
                }

                // Deserialize the fault
                var serializer = new NetDataContractSerializer();
                try
                {
                    return serializer.ReadObject(reader);
                }
                catch (FileNotFoundException)
                {
                    // Serializer was unable to find assembly where exception is defined 
                    return null;
                }
            }
        }

        #endregion

        #region IClientMessageInspector Members

        /// <summary>
        /// Create a copy of the original reply to allow 
        /// default processing of the message. Then its reads 
        /// the copied reply to find Fault Detail using the ReadFaultDetail method
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.IsFault)
            {
                // Create a copy of the original reply to allow default processing of the message
                var buffer = reply.CreateBufferedCopy(Int32.MaxValue);
                var copy = buffer.CreateMessage();  // Create a copy to work with
                reply = buffer.CreateMessage();         // Restore the original message

                var faultDetail = ReadFaultDetail(copy);
                var exception = faultDetail as Exception;
                if (exception != null)
                {
                    throw exception;
                }
            }
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            return null;
        }

        #endregion
    }
}

// end of namespace
