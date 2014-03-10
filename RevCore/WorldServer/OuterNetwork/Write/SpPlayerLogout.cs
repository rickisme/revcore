using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerLogout : OuterNetworkSendPacket
    {
        public SpPlayerLogout()
        {

        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 4);
        }
    }
}
