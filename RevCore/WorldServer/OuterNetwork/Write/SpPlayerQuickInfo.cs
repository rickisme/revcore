using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerQuickInfo : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerQuickInfo(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1);
            WriteD(writer, (int)Player.UID);

            WriteSN(writer, Player.PlayerData.Name);
            WriteC(writer, 0);

            WriteD(writer, 0); //Guild ID
            WriteSN(writer, ""); //Guild Name
            WriteC(writer, 0); //Guild Level

            WriteH(writer, 0);

            WriteH(writer, (byte)Player.PlayerData.Forces);
            WriteH(writer, (byte)Player.GetLevel());
            WriteH(writer, (byte)Player.GetJobLevel()); // Job Level
            WriteC(writer, (byte)Player.PlayerData.Class);
            WriteC(writer, 1); // Famous
            WriteC(writer, 0); // null

            WriteC(writer, (byte)Player.PlayerData.HairColor);
            WriteC(writer, (byte)Player.PlayerData.HairStyle);
            WriteC(writer, (byte)Player.PlayerData.Face);
            WriteC(writer, (byte)Player.PlayerData.Voice);
            WriteC(writer, (byte)Player.PlayerData.Gender);

            WriteF(writer, Player.Position.X);
            WriteF(writer, Player.Position.Z);
            WriteF(writer, Player.Position.Y);
            WriteD(writer, Player.Position.MapId);

            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);

            WriteB(writer, "0000000000000000FFFFFFFFFFFFFFFF0100000000000000FFFFFFFFFFFFFFFF0200000000000000FFFFFFFFFFFFFFFF");

            for (int i = 0; i < 15; i++)
            {
                WriteItemInfo(writer, Player.Inventory.EquipItems[i]);
            }

            WriteB(writer, new byte[864]);
        }
    }
}
