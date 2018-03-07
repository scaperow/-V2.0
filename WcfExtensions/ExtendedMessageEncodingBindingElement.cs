//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Xml;
using System.ServiceModel.Channels;


namespace WcfExtensions
{
    public sealed class ExtendedMessageEncodingBindingElement
        : MessageEncodingBindingElement //BindingElement
    {
        //We will use an inner binding element to store information required for the inner encoder
        MessageEncodingBindingElement innerBindingElement;
        private string _encoderType;

        //By default, use the default text encoder as the inner encoder
        public ExtendedMessageEncodingBindingElement(string encoderType)
            : this(new TextMessageEncodingBindingElement(), encoderType)
        {
        }

        public ExtendedMessageEncodingBindingElement()
            : this(new TextMessageEncodingBindingElement(), MessageEncodingBindingElementExtension.Config_Property_MessageEncoderType_DefaultValue)
        {
        }

        public ExtendedMessageEncodingBindingElement(MessageEncodingBindingElement messageEncoderBindingElement, string encoderType)
        {
            _encoderType = encoderType;
            this.innerBindingElement = messageEncoderBindingElement;
        }

        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get { return innerBindingElement; }
            set { innerBindingElement = value; }
        }

        //Main entry point into the encoder binding element. Called by WCF to get the factory that will create the
        //message encoder
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new ExtendedMessageEncoderFactory(innerBindingElement.CreateMessageEncoderFactory(), _encoderType);
        }
       
        public override MessageVersion MessageVersion
        {
            get { return innerBindingElement.MessageVersion; }
            set { innerBindingElement.MessageVersion = value; }
        }


        public override BindingElement Clone()
        {
            return new ExtendedMessageEncodingBindingElement(this.innerBindingElement, _encoderType);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return innerBindingElement.GetProperty<T>(context);
            }
            else 
            {
                return base.GetProperty<T>(context);
            }
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }
    }
}