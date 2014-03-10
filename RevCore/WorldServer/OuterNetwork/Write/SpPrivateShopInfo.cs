using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPrivateShopInfo : OuterNetworkSendPacket
    {
        protected int type;
        protected Player player;

        public SpPrivateShopInfo(Player player) 
        {
            this.type = 1;
            this.player = player;
        }
        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteC(writer, type);
            WriteD(writer, player.UID);
            WriteD(writer, player.UID);
            WriteS(writer, player.PrivateShop.Name);
        }
    }
}
