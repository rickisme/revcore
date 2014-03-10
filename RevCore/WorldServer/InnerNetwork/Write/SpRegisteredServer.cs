using System.IO;
using Data.Structures.Template.Servers;
using DatabaseFactory;

namespace WorldServer.InnerNetwork.Write
{
    public class SpRegisteredServer : InnerNetworkSendPacket
    {
        protected Server serverInfo;

        public SpRegisteredServer()
        {
            // todo load server & channel info
            serverInfo = DataBaseServer.GetServerInfo(Properties.Settings.Default.SERVER_ID);
            serverInfo.channels = DataBaseServer.GetServerChannel(Properties.Settings.Default.SERVER_ID);
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, serverInfo.id); // server id
        }
    }
}
