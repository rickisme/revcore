using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentServer.OuterNetwork.Write;

namespace AgentServer.OuterNetwork.Read
{
    public class RpSelectSrv : OuterNetworkRecvPacket
    {
        protected int svid;
        protected int chid;

        public override void Read()
        {
            svid = ReadD();
            chid = ReadD();
            ReadC();
        }

        public override void Process()
        {
            new SpSelectSrv(svid, chid).Send(Connection);
        }
    }
}
