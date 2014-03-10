using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpWeightMoney : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpWeightMoney(Player p)
        {
            Player = p;
        }

        public override void Write(BinaryWriter writer)
        {
            // todo คำนวนหาน้ำหนักไอเทม

            int weight = 0;

            foreach (var item in Player.Inventory.Items.Values)
            {
                weight += item.ItemTemplate.Weight;
            }

            WriteQ(writer, Player.Inventory.Money); // Money
            WriteD(writer, weight); // Current Weight
            WriteD(writer, Player.Inventory.MaxWeight); // Max Weight 
        }
    }
}
