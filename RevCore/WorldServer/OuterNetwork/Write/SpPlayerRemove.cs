using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerRemove : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerRemove(Player player)
        {
            Player = player;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1); // count
            WriteD(writer, (int)Player.UID);

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Player.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
