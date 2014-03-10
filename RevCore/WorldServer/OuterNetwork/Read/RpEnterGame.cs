using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global.Logic;

namespace WorldServer.OuterNetwork.Read
{
    public class RpEnterGame : OuterNetworkRecvPacket
    {
        protected int Index;

        public override void Read()
        {
            Index = ReadC();
        }

        public override void Process()
        {
            Connection.Account.Players[Index].Connection = Connection;
            Connection.Player = Connection.Account.Players[Index];
            PlayerLogic.PlayerEnterWorld(Connection.Player);
        }
    }
}
