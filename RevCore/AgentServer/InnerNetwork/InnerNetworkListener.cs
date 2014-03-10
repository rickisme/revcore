using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using Utilities;

namespace AgentServer.InnerNetwork
{
    public class InnerNetworkListener
    {
        protected string BindAddress;
        protected int BindPort;
        protected int MaxConnections;

        public IScsServer InnerServer;

        public InnerNetworkListener(string bindAddress, int bindPort, int maxConnections)
        {
            BindAddress = bindAddress;
            BindPort = bindPort;
            MaxConnections = maxConnections;
        }

        public void BeginListening()
        {
            Log.Info("Start InnerServer at {0}:{1}...", BindAddress, BindPort);
            InnerServer = ScsServerFactory.CreateServer(new ScsTcpEndPoint(BindAddress, BindPort));
            InnerServer.Start();

            InnerServer.ClientConnected += OnConnected;
            InnerServer.ClientDisconnected += OnDisconnected;
        }

        protected void OnConnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("InnerClient connected!");
            new InnerNetworkConnection(e.Client);
        }

        protected void OnDisconnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("InnerClient disconnected!");
        }
    }
}
