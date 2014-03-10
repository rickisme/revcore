using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities;

namespace AgentServer.OuterNetwork.Write
{
    public class SpServerList : OuterNetworkSendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            var svlist = AgentServer.SvrListInfo;
            WriteH(writer, svlist.Count);

            foreach(var sv in svlist)
            {
                WriteH(writer, sv.id);
                WriteS(writer, sv.name);
                WriteH(writer, 0);
                WriteH(writer, 0);
                WriteH(writer, 1);

                WriteH(writer, sv.channels.Count);

                foreach (var ch in sv.channels)
                {
                    int online = 0; // todo get user online in channel
                    int percent = ((online * 100) / ch.max_user);
                    WriteH(writer, ch.id);
                    WriteS(writer, ch.name);
                    WriteH(writer, percent);
                    WriteH(writer, ch.type);
                }
            }
        }
    }
}
