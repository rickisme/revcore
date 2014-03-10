using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpCreatePlayer : OuterNetworkSendPacket
    {
        protected bool IsSuccess;

        public SpCreatePlayer(bool ss)
        {
            IsSuccess = ss;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, (IsSuccess == true) ? 1 : 0);
        }
    }
}
