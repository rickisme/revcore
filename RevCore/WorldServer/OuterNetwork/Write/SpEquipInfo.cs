using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpEquipInfo : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpEquipInfo(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            for (int i = 0; i < 30; i++)
            {
                if (Player.Inventory.EquipItems.ContainsKey(i) && i <= 15)
                {
                    var item = Player.Inventory.GetEquipItem(i);
                    WriteItemInfo(writer, item);
                }
                else
                    WriteB(writer, new byte[88]);
            }

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Player.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
