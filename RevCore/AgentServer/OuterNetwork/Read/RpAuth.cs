using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using AgentServer.Services;

namespace AgentServer.OuterNetwork.Read
{
    public class RpAuth : OuterNetworkRecvPacket
    {
        protected string Name;
        protected string Md5Pass;

        public override void Read()
        {
            Name = ReadS();
            Md5Pass = ReadS();
        }

        public override void Process()
        {
            AgentServer.AccountService.Authorized(Connection, Name, Md5Pass);
        }
    }
}
