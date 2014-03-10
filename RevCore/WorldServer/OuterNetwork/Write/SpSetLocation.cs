using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpSetLocation : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpSetLocation(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteF(writer, Player.Position.X);
            WriteF(writer, Player.Position.Z);
            WriteF(writer, Player.Position.Y);
            WriteD(writer, Player.Position.MapId);
            WriteD(writer, 0);
            WriteD(writer, 0);
        }
    }
}
