using Data.Structures.Template.Item;
using Data.Structures.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpDropInfo : OuterNetworkSendPacket
    {
        protected List<Item> Items = new List<Item>();

        public SpDropInfo(Item item)
        {
            Items.Add(item);
        }

        public SpDropInfo(List<Item> items)
        {
            Items = items;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, Items.Count); // count

            foreach (var item in Items)
            {
                var template = ItemTemplate.Factory(item.ItemId);

                WriteQ(writer, item.UID);
                WriteQ(writer, item.ItemId);
                WriteD(writer, item.Count);
                WriteF(writer, item.Position.X);
                WriteF(writer, 15f);
                WriteF(writer, item.Position.Y);

                WriteB(writer, new byte[68]);
            }
        }
    }
}
