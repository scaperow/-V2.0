/**
 * File name: GZipMessageEncoder.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/12/2009 3:54:04 PM format: MM/dd/yyyy
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
using System.IO.Compression;
using System.ServiceModel.Channels;

#endregion

namespace WcfExtensions
{
    public class GZipMessageEncoder : MessageEncoder
    {
        #region Members & Variables

        static string GZipContentType = "application/x-gzip";

        //This implementation wraps an inner encoder that actually converts a WCF Message
        //into textual XML, binary XML or some other format. This implementation then compresses the results.
        //The opposite happens when reading messages.
        //This member stores this inner encoder.
        MessageEncoder innerEncoder;

        #endregion

        #region Constructors

        //We require an inner encoder to be supplied (see comment above)
        public GZipMessageEncoder(MessageEncoder messageEncoder)
            : base()
        {
            if (messageEncoder == null)
                throw new ArgumentNullException("messageEncoder", "A valid message encoder must be passed to the GZipEncoder");
            innerEncoder = messageEncoder;
        }

        #endregion

        #region Properties

        public override string ContentType
        {
            get { return GZipContentType; }
        }

        public override string MediaType
        {
            get { return GZipContentType; }
        }

        //SOAP version to use - we delegate to the inner encoder for this
        public override MessageVersion MessageVersion
        {
            get { return innerEncoder.MessageVersion; }
        }

        #endregion

        #region Methods 

        //Helper method to compress an array of bytes
        static ArraySegment<byte> CompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, int messageOffset)
        {
            //Display the Compressed and uncompressed sizes
            //Console.WriteLine("Original message is {0} bytes", buffer.Count);

            var memoryStream = new MemoryStream();
            memoryStream.Write(buffer.Array, 0, messageOffset);

            using (var gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gzStream.Write(buffer.Array, messageOffset, buffer.Count);
            }

            var compressedBytes = memoryStream.ToArray();
            var bufferedBytes = bufferManager.TakeBuffer(compressedBytes.Length);

            Array.Copy(compressedBytes, 0, bufferedBytes, 0, compressedBytes.Length);

            bufferManager.ReturnBuffer(buffer.Array);
            var byteArray = new ArraySegment<byte>(bufferedBytes, messageOffset, bufferedBytes.Length - messageOffset);

            //Console.WriteLine("GZipCompressed message is {0} bytes", byteArray.Count);

            return byteArray;
        }

        //Helper method to decompress an array of bytes
        static ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager)
        {
            //Console.WriteLine(string.Format("Compressed buffer size is {0} bytes", buffer.Count));
            var memoryStream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count - buffer.Offset);
            var decompressedStream = new MemoryStream();
            var totalRead = 0;
            var blockSize = 1024;
            var tempBuffer = bufferManager.TakeBuffer(blockSize);
            using (var gzStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                while (true)
                {
                    var bytesRead = gzStream.Read(tempBuffer, 0, blockSize);
                    if (bytesRead == 0)
                        break;
                    decompressedStream.Write(tempBuffer, 0, bytesRead);
                    totalRead += bytesRead;
                }
            }
            bufferManager.ReturnBuffer(tempBuffer);

            var decompressedBytes = decompressedStream.ToArray();
            var bufferManagerBuffer = bufferManager.TakeBuffer(decompressedBytes.Length + buffer.Offset);
            Array.Copy(buffer.Array, 0, bufferManagerBuffer, 0, buffer.Offset);
            Array.Copy(decompressedBytes, 0, bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);

            var byteArray = new ArraySegment<byte>(bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);
            bufferManager.ReturnBuffer(buffer.Array);
            //Console.WriteLine(string.Format("Decompressed buffer size is {0} bytes", byteArray.Count));
            return byteArray;
        }

        #endregion

        #region MessageEncoder Members

        //One of the two main entry points into the encoder. Called by WCF to decode a buffered byte array into a Message.
        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            //Decompress the buffer
            var decompressedBuffer = DecompressBuffer(buffer, bufferManager);
            //Use the inner encoder to decode the decompressed buffer
            var returnMessage = innerEncoder.ReadMessage(decompressedBuffer, bufferManager);
            returnMessage.Properties.Encoder = this;
            return returnMessage;
        }

        //One of the two main entry points into the encoder. Called by WCF to encode a Message into a buffered byte array.
        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            //Use the inner encoder to encode a Message into a buffered byte array
            var buffer = innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
            //Compress the resulting byte array
            var compressedBuffer = CompressBuffer(buffer, bufferManager, messageOffset);

            return compressedBuffer;
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            var gzStream = new GZipStream(stream, CompressionMode.Decompress, true);
            return innerEncoder.ReadMessage(gzStream, maxSizeOfHeaders);
        }

        public override void WriteMessage(Message message, System.IO.Stream stream)
        {
            using (var gzStream = new GZipStream(stream, CompressionMode.Compress, true))
            {
                innerEncoder.WriteMessage(message, gzStream);
            }

            // innerEncoder.WriteMessage(message, gzStream) depends on that it can flush data by flushing 
            // the stream passed in, but the implementation of GZipStream.Flush will not flush underlying
            // stream, so we need to flush here.
            stream.Flush();
        }

        #endregion
    }
}

// end of namespace
