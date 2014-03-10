using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpExit : OuterNetworkSendPacket
    {
        public SpExit()
        {

        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, 0);
            WriteC(writer, 0);
            WriteB(writer, Encoding.Default.GetBytes(State.Account.name));
        }
    }
}
