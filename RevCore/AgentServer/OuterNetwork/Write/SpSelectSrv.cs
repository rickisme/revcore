using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Structures.Template.Servers;

namespace AgentServer.OuterNetwork.Write
{
    public class SpSelectSrv : OuterNetworkSendPacket
    {
        protected Data.Structures.Template.Servers.Server SrvInfo;
        protected Data.Structures.Template.Servers.Channel ChnInfo;

        public SpSelectSrv(int svid, int chid)
        {
            SrvInfo = AgentServer.SvrListInfo.Where(v => v.id == svid).FirstOrDefault();
            ChnInfo = SrvInfo.channels.Where(v => v.id == chid).FirstOrDefault();
        }

        public override void Write(BinaryWriter writer)
        {
            WriteS(writer, SrvInfo.ip);
            WriteH(writer, ChnInfo.port);
            WriteS(writer, ChnInfo.name);
            WriteS(writer, ChnInfo.name);
        }
    }
}
