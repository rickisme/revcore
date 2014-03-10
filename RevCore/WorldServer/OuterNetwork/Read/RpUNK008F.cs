using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global.Logic;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork.Read
{
    public class RpUNK008F : OuterNetworkRecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
            //PlayerLogic.PlayerEnterWorld(Connection.Player);
            new SpUNK0020().Send(Connection);
        }
    }
}
