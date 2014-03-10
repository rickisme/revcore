using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using Utilities;

namespace WorldServer.OuterNetwork
{
    public class OuterNetworkListener
    {
        protected string BindAddress;
        protected int BindPort;
        protected int MaxConnections;

        public IScsServer OuterServer;

        public OuterNetworkListener(string bindAddress, int bindPort, int maxConnections)
        {
            BindAddress = bindAddress;
            BindPort = bindPort;
            MaxConnections = maxConnections;
        }

        public void BeginListening()
        {
            Log.Info("Start OuterServer at {0}:{1}...", BindAddress, BindPort);
            OuterServer = ScsServerFactory.CreateServer(new ScsTcpEndPoint(BindAddress, BindPort));
            OuterServer.Start();

            OuterServer.ClientConnected += OnConnected;
            OuterServer.ClientDisconnected += OnDisconnected;
        }

        protected void OnConnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("Client connected!");
            new OuterNetworkConnection(e.Client);
        }

        protected void OnDisconnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("Client disconnected!");
        }
    }
}
