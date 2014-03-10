using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerRunning : OuterNetworkSendPacket
    {
        protected bool IsRunning = false;

        public SpPlayerRunning(bool r)
        {
            IsRunning = r;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, (IsRunning) ? 1 : 0);
        }
    }
}
