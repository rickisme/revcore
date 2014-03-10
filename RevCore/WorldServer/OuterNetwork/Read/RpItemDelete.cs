using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpItemDelete : OuterNetworkRecvPacket
    {
        protected int Type;
        protected int Position;
        protected long ItemId;
        protected long ItemCount;

        /*
         * 44 42 0F 00 00 00 00 00 
         * 68 CA 9A 3B 00 00 00 00 
         * 02 00 00 00 00 00 00 00 
         * 01 
         * 02 
         * 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
         */
        public override void Read()
        {
            ReadQ();
            ItemId = ReadQ();
            ItemCount = ReadQ();
            Type = ReadC();
            Position = ReadC();
        }

        public override void Process()
        { 
        
        }
    }
}
