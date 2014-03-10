using Data.Structures.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpDropRemove : OuterNetworkSendPacket
    {
        protected Item Item;

        public SpDropRemove(Item item)
        {
            Item = item;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteQ(writer, Item.UID);
        }
    }
}
