using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpOpenOnlineShop : OuterNetworkRecvPacket
    {
        protected int type;
        public override void Read()
        {
            type = ReadD();
            ReadD();
        }

        public override void Process()
        {
            Global.Global.ShopService.OpenOnlineShop(Connection.Player, type);
        }
    }
}
