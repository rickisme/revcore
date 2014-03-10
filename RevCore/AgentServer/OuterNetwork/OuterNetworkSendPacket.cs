using System;
using System.IO;
using System.Text;
using Data.Interfaces;
using Utilities;

namespace AgentServer.OuterNetwork
{
    public abstract class OuterNetworkSendPacket : ISendPacket
    {
        private string EncodingName = "TIS-620";
        protected byte[] Data;
        protected object WriteLock = new object();

        public void Send(IConnection state)
        {
            if (state == null || !state.IsValid)
                return;

            if (!OuterNetworkOpcode.Send.ContainsKey(GetType()))
            {
                Log.Warn("UNKNOWN packet opcode: {0}", GetType().Name);
                return;
            }


            lock (WriteLock)
            {
                if (Data == null)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream, new UTF8Encoding()))
                            {
                                WriteH(writer, OuterNetworkOpcode.Send[GetType()]);
                                WriteH(writer, 0); //Reserved for length
                                Write(writer);
                            }

                            Data = stream.ToArray();
                            BitConverter.GetBytes((short)Data.Length).CopyTo(Data, 2);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn("Can't write packet: {0}", GetType().Name);
                        Log.WarnException("ASendPacket", ex);
                        return;
                    }
                }
            }

            //Log.Warn("PUSH packet opcode: {0}", GetType().Name);
            //Log.Debug("Data:\n{0}", Data.FormatHex());
            state.PushPacket(Data);
        }

        public abstract void Write(BinaryWriter writer);

        protected void WriteD(BinaryWriter writer, int val)
        {
            writer.Write(val);
        }

        protected void WriteH(BinaryWriter writer, short val)
        {
            writer.Write(val);
        }

        protected void WriteH(BinaryWriter writer, int val)
        {
            writer.Write((short)val);
        }

        protected void WriteC(BinaryWriter writer, byte val)
        {
            writer.Write(val);
        }

        protected void WriteDf(BinaryWriter writer, double val)
        {
            writer.Write(val);
        }

        protected void WriteF(BinaryWriter writer, float val)
        {
            writer.Write(val);
        }

        protected void WriteQ(BinaryWriter writer, long val)
        {
            writer.Write(val);
        }

        protected void WriteS(BinaryWriter writer, String text)
        {
            if (text.Length > 0)
            {
                Encoding encoding = Encoding.GetEncoding(EncodingName);
                writer.Write((short)text.Length);
                writer.Write(encoding.GetBytes(text));
            }
        }

        protected void WriteB(BinaryWriter writer, string hex)
        {
            writer.Write(hex.ToBytes());
        }

        protected void WriteB(BinaryWriter writer, byte[] data)
        {
            writer.Write(data);
        }
    }
}
