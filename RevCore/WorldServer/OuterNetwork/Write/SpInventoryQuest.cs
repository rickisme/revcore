using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpInventoryQuest : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpInventoryQuest(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 36);
            for (int i = 0; i < 36; i++)
            {
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0); // item id
                WriteD(writer, 0);
                WriteD(writer, 0); // count
            }
        }
    }
}
