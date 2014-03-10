using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpBindPoint : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpBindPoint(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteH(writer, 0);
        }
    }
}
