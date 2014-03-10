using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpAuth : OuterNetworkRecvPacket
    {
        protected string Name;
        protected string Md5Pass;
        protected int chnId;
        protected string IpAddress;

        public override void Read()
        {
            Name = Encoding.Default.GetString(ReadB(31)).Replace("\0", "");
            Md5Pass = Encoding.Default.GetString(ReadB(31));
            ReadH();
            ReadH();
            chnId = ReadH();
            IpAddress = Encoding.Default.GetString(ReadB(15)).Replace("\0", "");
        }

        public override void Process()
        {
            Connection.IpAddress = IpAddress;
            Connection.ChannelID = chnId;
            Global.Global.AccountService.Authorized(Connection, Name, Md5Pass);
        }
    }
}
