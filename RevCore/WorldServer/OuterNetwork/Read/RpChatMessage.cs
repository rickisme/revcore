using Data.Enums;
using Data.Structures.Player;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork.Read
{
    public class RpChatMessage : OuterNetworkRecvPacket
    {
        protected ChatType Type;
        protected string RecvName;
        protected string Message;

        public override void Read()
        {
            Type = (ChatType)ReadC();
            RecvName = ReadSN();
            ReadB(7);
            Message = ReadS();
        }

        public override void Process()
        {
            Log.Debug("Message [{0}]: {1}", Message.Length, Message);
            PlayerLogic.ProcessChatMessage(Connection, RecvName, Message, Type);
        }
    }
}
