using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global.Logic;

namespace WorldServer.OuterNetwork.Read
{
    public class RpCheckName : OuterNetworkRecvPacket
    {
        protected string Name;

        public override void Read()
        {
            Name = Encoding.Default.GetString(ReadB(15));
        }

        public override void Process()
        {
            PlayerLogic.CheckName(Connection, Name);
        }
    }
}
