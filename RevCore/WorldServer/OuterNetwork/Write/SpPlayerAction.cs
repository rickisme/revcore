using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerAction : OuterNetworkSendPacket
    {
        protected Player Player;
        protected int Id;

        public SpPlayerAction(Player player, int id)
        {
            Player = player;
            Id = id;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteC(writer, (byte)Id);

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Player.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
