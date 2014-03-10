using Data.Structures.Player;
using Data.Structures.World;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpItemPickUp : OuterNetworkRecvPacket
    {
        public long UId;

        public override void Read()
        {
            UId = ReadQ();
        }

        public override void Process()
        {
            Item item = Connection.Player.Instance.Items
                .Where(v => v.UID == UId)
                .FirstOrDefault();

            if(item != null)
                PlayerLogic.PickUpItem(Connection, item);
        }
    }
}
