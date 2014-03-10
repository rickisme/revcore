using Data.Enums;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities;

namespace WorldServer.OuterNetwork
{
    public class OuterWireProtocol : IScsWireProtocol
    {
        protected MemoryStream Stream = new MemoryStream();

        public byte[] GetBytes(IScsMessage message)
        {
            return ((OuterNetworkMessage)message).Data;
        }

        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            //Log.Debug("CreateMessages:\n{0}", receivedBytes.FormatHex());

            byte[] body = new byte[receivedBytes.Length - 4];
            Buffer.BlockCopy(receivedBytes, 2, body, 0, body.Length);
            Stream.Write(body, 0, body.Length);
            //Stream.Write(receivedBytes, 0, receivedBytes.Length);

            List<IScsMessage> messages = new List<IScsMessage>();

            while (ReadMessage(messages)) ;

            return messages;
        }

        private bool ReadMessage(List<IScsMessage> messages)
        {
            Stream.Position = 0;

            if (Stream.Length < 4)
                return false;

            int headerBytesLength = 8;
            int opcodeoffset = 4;
            int lengthoffset = 6;

            switch (WorldServer.CountryCode)
            {
                case CountryCode.CN:
                    {
                        headerBytesLength = 9;
                        opcodeoffset = 5;
                        lengthoffset = 7;
                    }
                    break;
                case CountryCode.EN:
                    {
                        headerBytesLength = 8;
                        opcodeoffset = 4;
                        lengthoffset = 6;
                    }
                    break;
                case CountryCode.TH:
                    {
                        headerBytesLength = 8;
                        opcodeoffset = 4;
                        lengthoffset = 6;
                    }
                    break;
                case CountryCode.TW:
                    {
                        headerBytesLength = 9;
                        opcodeoffset = 5;
                        lengthoffset = 7;
                    }
                    break;
            }

            byte[] headerBytes = new byte[headerBytesLength];
            Stream.Read(headerBytes, 0, headerBytesLength);

            int alllength = BitConverter.ToUInt16(headerBytes, 0);
            int sessionId = BitConverter.ToUInt16(headerBytes, 2);
            int opcode = BitConverter.ToUInt16(headerBytes, opcodeoffset);
            int length = BitConverter.ToUInt16(headerBytes, lengthoffset);

            if (opcode != 8 && Stream.Length < length)
                return false;

            OuterNetworkMessage message = new OuterNetworkMessage
            {
                TotalLength = (short)alllength,
                UserSession = (short)sessionId,
                Opcode = (short)opcode,
                Data = new byte[alllength - 6]
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
            Stream.Write(remaining, 4, remaining.Length - 4); // read next packet in onetime send
        }
    }
}
