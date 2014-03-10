using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpCraftList : OuterNetworkSendPacket
    {
        protected Player Player;
        public SpCraftList(Player Player) 
        {
            this.Player = Player;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteH(writer, Player.Crafts.Count);

            WriteQ(writer, 0);
            WriteQ(writer, 0);
            WriteD(writer, 0);
        }
    }
}