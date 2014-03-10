using System;
using System.IO;
using System.Text;
using Data.Interfaces;
using Utilities;

namespace WorldServer.InnerNetwork
{
    public abstract class InnerNetworkSendPacket : ISendPacket
    {
        protected byte[] Data;
        protected object WriteLock = new object();

        public void Send(IConnection state)
        {
            if (state == null || !state.IsValid)
                return;

            if (!InnerNetworkOpcode.Send.ContainsKey(GetType()))
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
                                WriteH(writer, 0); //Reserved for length
                                WriteH(writer, InnerNetworkOpcode.Send[GetType()]);
                                Write(writer);
                            }

                            Data = stream.ToArray();
                            BitConverter.GetBytes((short)Data.Length).CopyTo(Data, 0);
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
                Encoding encoding = Encoding.UTF8;
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
