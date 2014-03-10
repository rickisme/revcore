using Data.Enums;
using Data.Structures.World;

namespace WorldServer.OuterNetwork.Write
{
    public class SpItemPickupMsg : OuterNetworkSendPacket
    {
        protected ItemPickUp type;
        protected Item Item;

        public SpItemPickupMsg(ItemPickUp t, Item item)
        {
            type = t;
            Item = item;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, (int)type);

            if (Item.ItemId != 2000000000)
                WriteQ(writer, Item.UID);
            else
                WriteQ(writer, 0);

            WriteQ(writer, Item.ItemId);
            WriteQ(writer, Item.Count);
            WriteB(writer, new byte[34]);
        }
    }
}
