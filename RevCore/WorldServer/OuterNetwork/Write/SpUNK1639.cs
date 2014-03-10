using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    class SpUNK1639 : OuterNetworkSendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteH(writer, 0x3A1C);
            WriteH(writer, 0);
            WriteH(writer, 1);
            WriteH(writer, 0);
        }
    }
}
