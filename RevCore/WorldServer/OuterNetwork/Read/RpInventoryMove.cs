using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.OuterNetwork.Read
{
    public class RpInventoryMove : OuterNetworkRecvPacket
    {
        protected int fromInventory;
        protected int from;

        protected int toInventory;
        protected int to;

        protected MoveItemArgs Args = new MoveItemArgs();

        public override void Read()
        {
            Args.IsFromInventory = (byte)ReadC();
            Args.FromSlot = (byte)ReadC();
            Args.IsToInventory = (byte)ReadC();
            Args.ToSlot = (byte)ReadC();
            ReadD(); // UNK
        }

        public override void Process()
        {
            Player player = Connection.Player;
            Global.Global.StorageService.ReplaceItem(player, player.Inventory, Args);
        }
    }
}
