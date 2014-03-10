using System;
using System.Collections.Generic;
using System.IO;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;
using Utilities;

namespace AgentServer.InnerNetwork
{
    public class InnerWireProtocol : IScsWireProtocol
    {
        protected MemoryStream Stream = new MemoryStream();

        public byte[] GetBytes(IScsMessage message)
        {
            return ((InnerNetworkMessage)message).Data;
        }

        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            Stream.Write(receivedBytes, 0, receivedBytes.Length);

            List<IScsMessage> messages = new List<IScsMessage>();

            while (ReadMessage(messages));

            return messages;
        }

        private bool ReadMessage(List<IScsMessage> messages)
        {
            Stream.Position = 0;

            if (Stream.Length < 4)
                return false;

            byte[] headerBytes = new byte[4];
            Stream.Read(headerBytes, 0, 4);

            int length = BitConverter.ToUInt16(headerBytes, 0);

            if (Stream.Length < length)
                return false;

            InnerNetworkMessage message = new InnerNetworkMessage
            {
                OpCode = BitConverter.ToInt16(headerBytes, 2),
                Data = new byte[length - 4]
            };

            Stream.Read(message.Data, 0, message.Data.Length);

            messages.Add(message);

            TrimStream();

            return true;
        }

        public void Reset()
        {

        }

        private void TrimStream()
        {
            if (Stream.Position == Stream.Length)
            {
                Stream = new MemoryStream();
                return;
            }

            byte[] remaining = new byte[Stream.Length - Stream.Position];
            Stream.Read(remaining, 0, remaining.Length);
            Stream = new MemoryStream();
            Stream.Write(remaining, 0, remaining.Length);
        }
    }
}
