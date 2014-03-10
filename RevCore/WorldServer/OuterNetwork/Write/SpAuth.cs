using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities;

namespace WorldServer.OuterNetwork.Write
{
    public class SpAuth : OuterNetworkSendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 1); // gender
            WriteH(writer, 0);

            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 9);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0); //1

            WriteH(writer, 0);
            WriteH(writer, 14);
            WriteH(writer, 0);
            WriteH(writer, 1);
            WriteH(writer, 4369);
            WriteH(writer, 0);
            WriteH(writer, 4369);
            WriteH(writer, 0);
            WriteH(writer, 4369);

            WriteC(writer, 0xff);
            WriteC(writer, 0xff);

            WriteD(writer, 0);
            WriteD(writer, Funcs.GetRoundedLocal());
            WriteD(writer, 28);
            WriteB(writer, Funcs.NextBytes(4));
            WriteH(writer, 0x5041);
        }
    }
}
