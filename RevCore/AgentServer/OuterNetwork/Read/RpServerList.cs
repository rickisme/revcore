using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentServer.OuterNetwork.Write;

namespace AgentServer.OuterNetwork.Read
{
    public class RpServerList : OuterNetworkRecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
            new SpServerList().Send(Connection);
        }
    }
}
