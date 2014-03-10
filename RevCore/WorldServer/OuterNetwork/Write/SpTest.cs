using Data.Interfaces;
using System;
using System.IO;
using Utilities;

namespace WorldServer.OuterNetwork.Write
{
    public class SpTest : OuterNetworkSendPacket
    {
        protected short Opcode;
        protected byte[] Data;

        public SpTest(byte[] data, bool addLength = true)
        {
            Data = new byte[data.Length];
            Buffer.BlockCopy(data, 0, Data, 0, data.Length);
        }

        public SpTest(string hex, bool addLength = true)
            : this(hex.ToBytes(), addLength)
        {
        }

        public SpTest(short opcode, string hex, bool addLength = true)
            : this(hex.ToBytes(), addLength)
        {
            Opcode = opcode;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteB(writer, Data);

            writer.Seek(4, SeekOrigin.Begin);
            WriteH(writer, Opcode);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
