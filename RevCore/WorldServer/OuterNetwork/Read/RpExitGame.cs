using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpExitGame : OuterNetworkRecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
            AccountLogic.ExitPlayer(Connection);
        }
    }
}
