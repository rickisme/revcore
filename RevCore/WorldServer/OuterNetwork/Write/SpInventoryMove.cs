using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpInventoryMove : OuterNetworkSendPacket
    {
        protected MoveItemArgs Args;

        public SpInventoryMove(MoveItemArgs args)
        {
            Args = args;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1);
            WriteC(writer, Args.IsFromInventory);
            WriteC(writer, Args.FromSlot);
            WriteC(writer, Args.IsToInventory);
            WriteC(writer, Args.ToSlot);

            var item = Args.ItemToMove;

            WriteD(writer, 1);
            WriteItemInfo(writer, item);
        }
    }
}
