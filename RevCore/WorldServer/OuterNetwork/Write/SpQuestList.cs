using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpQuestList : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpQuestList(Player Player)
        {
            this.Player = Player;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, Player.Quests.Count); // questlist countc

            foreach (var quest in Player.Quests.Values)
            {
                WriteH(writer, quest.Key); // Quest ID
                WriteH(writer, quest.Value); // Step Quest
            }

            WriteD(writer, 0);
        }
    }
}
