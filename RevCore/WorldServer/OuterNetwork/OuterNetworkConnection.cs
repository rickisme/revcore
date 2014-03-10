using Data.Interfaces;
using Data.Structures.Account;
using Data.Structures.Player;
using Global.Logic;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using Utilities;

namespace WorldServer.OuterNetwork
{
    public class OuterNetworkConnection : IConnection
    {
        public static List<OuterNetworkConnection> OuterConnections = new List<OuterNetworkConnection>();

        /*********************************************************
         * GAME VARIABLE
         */
        public Account Account { get; set; }

        public Player Player { get; set; }

        public string IpAddress;

        public int ChannelID;

        public int UID
        {
            get { return Account.SessionID; }
        }

        /*********************************************************/

        protected IScsServerClient Client;

        protected List<byte[]> SendData = new List<byte[]>();

        protected int SendDataSize;

        protected object SendLock = new object();

        public byte[] Buffer;

        public static Thread SendAllThread = new Thread(SendAll);

        protected static void SendAll()
        {
            while (true)
            {
                for (int i = 0; i < OuterConnections.Count; i++)
                {
                    try
                    {
                        if (!OuterConnections[i].Send())
                            OuterConnections.RemoveAt(i--);
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorException("Connection: SendAll:", ex);
                    }
                }

                Thread.Sleep(10);
            }
        }

        public OuterNetworkConnection(IScsServerClient client)
        {

            Client = client;
            Client.WireProtocol = new OuterWireProtocol();

            Client.Disconnected += OnDisconnected;
            Client.MessageReceived += OnMessageReceived;

            OuterConnections.Add(this);
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            AccountLogic.ClientDisconnected(this);

            if (Account != null)
                Account.Connection = null;
            Account = null;

            if (Player != null)
                Player.Connection = null;
            Player = null;

            Buffer = null;
            Client = null;
            SendData = null;
            SendLock = null;
        }

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            OuterNetworkMessage message = (OuterNetworkMessage)e.Message;
            Buffer = message.Data;

            if (OuterNetworkOpcode.Recv.ContainsKey(message.Opcode))
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(message.Opcode).ToHex();
                Log.Debug("Recieve [{0}]: 0x{1}{2} [{3}]",
                                 OuterNetworkOpcode.Recv[message.Opcode].Name,
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Buffer.Length);

                Log.Debug("Data:\n{0}", Buffer.FormatHex());

                ((OuterNetworkRecvPacket)Activator.CreateInstance(OuterNetworkOpcode.Recv[message.Opcode])).Process(this);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(message.Opcode).ToHex();
                Log.Debug("Unknown InnerOpcode: 0x{0}{1} [{2}]",
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Buffer.Length);

                Log.Debug("Data:\n{0}", Buffer.FormatHex());
            }
        }

        private bool Send()
        {
            OuterNetworkMessage message;

            if (SendLock == null)
                return false;

            lock (SendLock)
            {
                if (SendData.Count == 0)
                    return Client.CommunicationState == CommunicationStates.Connected;

                message = new OuterNetworkMessage { Data = new byte[SendDataSize] };

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
                Client.SendMessage(message);
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
            Client.Disconnect();
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
            Ping ping = new Ping();

            //"tcp://127.0.0.1:27230"
            string ipAddress = Client.RemoteEndPoint.ToString().Substring(6);
            ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(':'));

            PingReply pingReply = ping.Send(ipAddress);

            return (pingReply != null) ? pingReply.RoundtripTime : 0;
        }
    }
}
