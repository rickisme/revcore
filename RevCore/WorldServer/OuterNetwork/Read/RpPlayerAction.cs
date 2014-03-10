using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork.Read
{
    public class RpPlayerAction : OuterNetworkRecvPacket
    {
        protected int Id;

        public override void Read()
        {
            Id = ReadC();
        }

        public override void Process()
        {
            PlayerLogic.PlayerAction(Connection.Player, Id);
        }
    }
}
