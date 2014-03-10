using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hik.Communication.Scs.Server;
using Utilities;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;

namespace AgentServer.OuterNetwork
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
            Log.Info("InnerClient connected!");
            new OuterNetworkConnection(e.Client);
        }

        protected void OnDisconnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("InnerClient disconnected!");
        }
    }
}
