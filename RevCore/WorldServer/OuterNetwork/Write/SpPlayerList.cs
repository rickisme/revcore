using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Structures.Player;
using System.IO;
using Utilities;

namespace WorldServer.OuterNetwork.Write
{
    public enum CharacterListResponse
    {
        None,
        Exists
    }

    public class SpPlayerList : OuterNetworkSendPacket
    {
        protected CharacterListResponse Response;

        protected Player Player;

        protected int Index;

        public SpPlayerList(int index, Player player, CharacterListResponse resp)
        {
            Index = index;
            Player = player;
            Response = resp;
        }

        public override void Write(BinaryWriter writer)
        {
            if (Response == CharacterListResponse.None)
            {
                WriteC(writer, 0xff);
            }
            else
            {
                WriteC(writer, (byte)Index);
                WriteSN(writer, Player.PlayerData.Name);
                WriteC(writer, 0);
                WriteD(writer, 0);
                WriteSN(writer, "");
                WriteC(writer, 0);
                WriteH(writer, 0);

                WriteH(writer, 1);
                WriteH(writer, Player.GetLevel());

                WriteC(writer, (byte)Player.PlayerData.Forces);
                WriteC(writer, 0); // famous
                WriteC(writer, (byte)Player.PlayerData.Class);

                // Appearance
                WriteC(writer, (byte)Player.PlayerData.HairStyle);
                WriteC(writer, (byte)Player.PlayerData.HairColor);
                WriteC(writer, (byte)Player.PlayerData.Face);
                WriteC(writer, (byte)Player.PlayerData.Voice);
                WriteC(writer, 0);
                WriteC(writer, (byte)Player.PlayerData.Title);
                WriteC(writer, (byte)Player.PlayerData.Gender);

                WriteF(writer, Player.Position.X);
                WriteF(writer, Player.Position.Z);
                WriteF(writer, Player.Position.Y);
                WriteD(writer, Player.Position.MapId);

                WriteB(writer, new byte[12]); // UNK

                WriteB(writer, "0000000000000000FFFFFFFFFFFFFFFF0100000000000000FFFFFFFFFFFFFFFF0200000000000000FFFFFFFFFFFFFFFF");

                WriteB(writer, new byte[4]);
                WriteH(writer, Player.MaxHp);
                WriteH(writer, Player.MaxMp);
                WriteD(writer, Player.MaxSp);
                WriteQ(writer, Player.GameStats.Exp);

                WriteH(writer, Player.LifeStats.Hp);
                WriteH(writer, Player.LifeStats.Mp);
                WriteD(writer, Player.LifeStats.Sp);
                WriteQ(writer, Player.Exp);

                WriteD(writer, 0);
                WriteB(writer, new byte[16]);

                var Equiped = Player.Inventory.EquipItems.Values.ToList();
                foreach (var item in Equiped)
                {
                    WriteItemInfo(writer, item);
                }
            }
        }
    }
}
