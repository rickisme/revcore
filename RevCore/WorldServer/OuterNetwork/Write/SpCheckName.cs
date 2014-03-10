using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Enums;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpCheckName : OuterNetworkSendPacket
    {
        protected string Name;
        protected CheckNameResult Result;

        public SpCheckName(string name, CheckNameResult rs)
        {
            Name = name;
            Result = rs;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteH(writer, (short)Result);
            WriteH(writer, 0);

            byte[] names = new byte[15];
            byte[] temp = Encoding.Default.GetBytes(Name);
            Buffer.BlockCopy(temp, 0, names, 0, temp.Length);

            WriteB(writer, names);
        }
    }
}
