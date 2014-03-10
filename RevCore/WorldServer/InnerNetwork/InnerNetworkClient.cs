using Data.Interfaces;
using Data.Structures.Account;
using Data.Structures.Player;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Utilities;
using WorldServer.InnerNetwork.Write;

namespace WorldServer.InnerNetwork
{
    public class InnerNetworkClient : IConnection
    {
        public static InnerNetworkClient innerNetworkClient;

        protected string BindAddress;
        protected int BindPort;

        public IScsClient InnerClient;

        public byte[] Buffer;
        protected object SendLock = new object();
        protected List<byte[]> SendData = new List<byte[]>();
        protected int SendDataSize;

        public InnerNetworkClient(string Address, int Port)
        {
            BindAddress = Address;
            BindPort = Port;

            InnerClient = ScsClientFactory.CreateClient(new ScsTcpEndPoint(BindAddress, BindPort));
            InnerClient.WireProtocol = new InnerWireProtocol();

            InnerClient.Connected += OnConnected;
            InnerClient.Disconnected += OnDisconnected;
            InnerClient.MessageReceived += OnMessageReceived;

            innerNetworkClient = this;
        }

        int retry = 0;
        public void BeginConnect()
        {
            try
            {
                if (retry == 0)
                    Log.Info("Connecting AgentServer...");
                else
                    Log.Info("Try Connecting AgentServer {0} ...", retry);

                InnerClient.Connect();
            }
            catch
            {
                retry++;
                BeginConnect();
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            retry = 0;
            Log.Info("Connected AgentServer at {0}:{1}...", BindAddress, BindPort);
            new SpRegisteredServer().Send(this);
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            try
            {
                if (retry == 0)
                    Log.Info("Connecting AgentServer...");
                else
                    Log.Info("Try Connecting AgentServer {0} ...", retry);

                InnerClient.Connect();
            }
            catch
            {
                retry++;
                BeginConnect();
            }
        }

        protected void OnMessageReceived(object sender, MessageEventArgs e)
        {

        }

        private bool Send()
        {
            InnerNetworkMessage message;

            if (SendLock == null)
                return false;

            lock (SendLock)
            {
                if (SendData.Count == 0)
                    return InnerClient.CommunicationState == CommunicationStates.Connected;

                message = new InnerNetworkMessage { Data = new byte[SendDataSize] };

                int pointer = 0;
                for (int i = 0; i < SendData.Count; i++)
                {
                    Array.Copy(SendData[i], 0, message.Data, pointer, SendData[i].Length);
                    pointer += SendData[i].Length;
                }

                SendData.Clear();
                SendDataSize = 0;
            }

            try
            {
                InnerClient.SendMessage(message);
            }
            catch
            {
                //Already closed
                return false;
            }

            return true;
        }

        public bool IsValid
        {
            get { return true; }
        }


        public void Close()
        {
            InnerClient.Disconnect();
        }

        public void PushPacket(byte[] data)
        {
            //Already closed
            if (SendLock == null)
                return;

            lock (SendLock)
            {
                SendData.Add(data);
                SendDataSize += data.Length;
            }
        }

        public long Ping()
        {
            return 0L;
        }

        public static Thread SendAllThread = new Thread(SendAll);

        protected static void SendAll()
        {
            while (true)
            {
                innerNetworkClient.Send();

                Thread.Sleep(10);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        public Account Account { get; set; }

        public Player Player { get; set; }
    }
}
