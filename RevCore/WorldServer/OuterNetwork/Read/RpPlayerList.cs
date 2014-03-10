using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpPlayerList : OuterNetworkRecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
            Global.Global.FeedbackService.SendPlayerLists(Connection);
        }
    }
}
