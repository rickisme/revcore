using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPremiumRevive : OuterNetworkSendPacket
    {
        protected bool Success = false;
        protected long ItemId;

        public SpPremiumRevive(bool success, long itemid)
        {
            Success = success;
            ItemId = itemid;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, (Success) ? 1 : 4);
            WriteQ(writer, ItemId);
            WriteQ(writer, 5);
        }
    }
}
