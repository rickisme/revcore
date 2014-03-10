using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork.Read
{
    class RpUNK1638 : OuterNetworkRecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
            //PlayerLogic.PlayerEnterWorld(Connection.Player);
            new SpUNK1639().Send(Connection);
        }
    }
}
