/**
 * File name: ExtendedMessageEncoderFactory.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/12/2009 4:02:39 PM format: MM/dd/yyyy
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
using System.ServiceModel.Channels;

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    internal class ExtendedMessageEncoderFactory : MessageEncoderFactory
    {
        private MessageEncoder encoder;

        //The GZip encoder wraps an inner encoder
        //We require a factory to be passed in that will create this inner encoder
        public ExtendedMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory, string encoderType)
        {
            if (messageEncoderFactory == null)
                throw new ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder");
            var messageEncoderType = Type.GetType(encoderType);
            encoder = (MessageEncoder)Activator.CreateInstance(messageEncoderType, messageEncoderFactory.Encoder);
        }

        //The service framework uses this property to obtain an encoder from this encoder factory
        public override MessageEncoder Encoder
        {
            get { return encoder; }
        }

        public override MessageVersion MessageVersion
        {
            get { return encoder.MessageVersion; }
        }
    }
}

// end of namespace
