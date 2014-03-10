using Data.Structures.Template.Servers;
using DatabaseFactory;
using System.Collections.Generic;
using Utilities;

namespace AgentServer.InnerNetwork.Read
{
    public class RpRegisteredServer : InnerNetworkRecvPacket
    {
        private int serverId;
        public override void Read()
        {
            serverId = ReadD();
        }

        public override void Process()
        {
            Server serverInfo = DataBaseServer.GetServerInfo(serverId);
            serverInfo.channels = DataBaseServer.GetServerChannel(serverInfo.id);

            if (!AgentServer.SvrListInfo.Contains(serverInfo))
            {
                AgentServer.SvrListInfo.Add(serverInfo);
                Log.Info("Registered Server ID: {0} - {1} Channels..", serverInfo.id, serverInfo.channels.Count);
            }
        }
    }
}
