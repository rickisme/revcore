using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpItemEffect : OuterNetworkSendPacket
    {
        protected long ItemId;
        public SpItemEffect(long ItemId) 
        {
            this.ItemId = ItemId;
        }
        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteQ(writer, ItemId);
        }
    }
}
