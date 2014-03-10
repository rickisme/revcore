using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpUseItem : OuterNetworkSendPacket
    {
        protected Player Player;

        protected int Type;
        protected int Position;
        protected long ItemId;
        protected long ItemCount;
        public SpUseItem(Player Player, int Type, int Position, long ItemId, long ItemCount)
        {
            this.Player = Player;
            this.Type = Type;
            this.Position = Position;
            this.ItemId = ItemId;
            this.ItemCount = ItemCount;
        }
        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteC(writer, 1);
            WriteC(writer, Type);
            WriteH(writer, 0);
            WriteQ(writer, ItemId);
            WriteD(writer, 1);
            WriteQ(writer, ItemCount);
            WriteD(writer, 0);
        }
    }
}