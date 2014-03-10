using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpPlayerDiedDialog : OuterNetworkRecvPacket
    {
        protected int Type;

        public override void Read()
        {
            Type = ReadD();
        }

        public override void Process()
        {
            PlayerLogic.Ressurect(Connection.Player, Type);
        }
    }
}
