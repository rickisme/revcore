using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpItemDelete: OuterNetworkSendPacket
    {
        protected Player Player;
        protected int Count;
        protected int MaxCount;
        public SpItemDelete(Player player, int count, int maxCount)
        {
            Player = player;
            Count = count;
            MaxCount = maxCount;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1);
            WriteD(writer, Count);
            WriteD(writer, MaxCount);
            WriteD(writer, 1);

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Player.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
