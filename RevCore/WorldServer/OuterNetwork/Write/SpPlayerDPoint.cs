using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerDPoint : OuterNetworkSendPacket
    {
        private Player player;

        public SpPlayerDPoint(Player player)
        {
            // TODO: Complete member initialization
            this.player = player;
        }
        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteQ(writer, player.DPoint);
        }
    }
}
