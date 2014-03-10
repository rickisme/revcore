using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerInfo : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerInfo(Player p)
        {
            Player = p;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 1);

            string name = Player.PlayerData.Name;
            if (Global.Global.AdminEngine.IsGM(Player))
                name = "[GM]" + Player.PlayerData.Name;

            if (Global.Global.AdminEngine.IsDev(Player))
                name = "[DEV]" + Player.PlayerData.Name;

            WriteD(writer, (int)Player.UID);
            WriteSN(writer, name);
            WriteC(writer, 0);

            WriteD(writer, 0); // Guild ID
            WriteSN(writer, string.Empty); // Guild Name
            WriteC(writer, 0);  //Guild Level

            WriteH(writer, 0); // SERVER ID ?

            WriteC(writer, Player.PlayerData.Forces);
            WriteC(writer, Player.GetLevel());
            WriteC(writer, Player.GetJobLevel()); // Job Level
            WriteC(writer, (byte)Player.PlayerData.Class);

            WriteC(writer, 1); 
            WriteC(writer, 0);

            WriteH(writer, Player.PlayerData.HairColor);
            WriteH(writer, Player.PlayerData.HairStyle);
            WriteC(writer, 1);
            WriteC(writer, (byte)Player.PlayerData.Gender);

            WriteH(writer, 0);

            WriteF(writer, Player.Position.X);
            WriteF(writer, Player.Position.Z);
            WriteF(writer, Player.Position.Y);
            WriteD(writer, Player.Position.MapId);

            var equips = Player.Inventory.EquipItems;

            WriteQ(writer, (equips[0] != null) ? equips[0].ItemId : 0); // Equip slot 0
            WriteQ(writer, (equips[1] != null) ? equips[1].ItemId : 0); // Equip slot 1
            WriteQ(writer, (equips[2] != null) ? equips[2].ItemId : 0); // Equip slot 2
            WriteQ(writer, (equips[4] != null) ? equips[4].ItemId : 0); // Equip slot 4
            WriteQ(writer, (equips[3] != null) ? equips[3].ItemId : 0); // Equip slot 3
            WriteQ(writer, (equips[5] != null) ? equips[5].ItemId : 0); // Equip slot 5
            WriteD(writer, (equips[3] != null) ? equips[3].Upgrade : 0); // Equip slot 3 LevelUpgrade
            WriteQ(writer, (equips[11] != null) ? equips[11].ItemId : 0); // Equip slot 11

            int setting = Settings.GetSettings(Player.Settings);
            WriteC(writer, setting);
            WriteC(writer, Player.Settings.FameSwitch);
            WriteH(writer, 0);

            WriteF(writer, Player.Position.X2);
            WriteF(writer, Player.Position.Z2);
            WriteF(writer, Player.Position.Y2);

            WriteD(writer, 0);
            WriteD(writer, 0);

            WriteD(writer, 0xff); // PET
            WriteD(writer, 0);
            WriteQ(writer, (equips[13] != null) ? equips[13].ItemId : 0); //  Equip slot 13

            WriteH(writer, 0); // Word gang door service
            WriteH(writer, 0); // Gang colors door service

            WriteD(writer, Player.HonorPoint); // Player_WuXun
            WriteD(writer, 1); // Forces side

            WriteD(writer, 0); // People head picture
            WriteD(writer, 0); // StealthMode

            WriteB(writer, "0000000000000000FFFFFFFFFFFFFFFF0100000000000000FFFFFFFFFFFFFFFF0200000000000000FFFFFFFFFFFFFFFF");

            WriteD(writer, 1);

            // Maried
            WriteC(writer, 0);
            WriteSN(writer, "");

            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);

            WriteC(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 0);

            WriteH(writer, 0);
        }
    }
}
