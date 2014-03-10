using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpPlayerLogout : OuterNetworkRecvPacket
    {
        public override void Read()
        {

        }

        public override void Process()
        {
            AccountLogic.LogoutPlayer(Connection);
        }
    }
}
