using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork.Read
{
    public class RpUseItem : OuterNetworkRecvPacket
    {
        protected int Type;
        protected int Position;
        protected long ItemId;
        protected long ItemCount;

         /*
          * 01 
          * 00 
          * 00 00 
          * 65 CA 9A 3B 00 00 00 00 
          * 00 00 00 00 
          * 05 00 00 00 00 00 00 00
          */ 
        public override void Read()
        {
            Type = ReadC();
            Position = ReadC();
            ReadH();
            ItemId = ReadQ();
            ReadD();
            ItemCount = ReadQ();
            ReadD();
        }

        public override void Process()
        {
            Global.Global.StorageService.UseItem(Connection.Player, Type, Position, ItemId, ItemCount);
        }
    }
}
