using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpServerTime : OuterNetworkSendPacket
    {
        protected int Time;

        public SpServerTime(int time)
        {
            Time = time;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, Time);
        }
    }
}
