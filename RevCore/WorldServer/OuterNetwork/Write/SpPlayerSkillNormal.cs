using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerSkillNormal : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerSkillNormal(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteH(writer, 100);
            WriteH(writer, 15213);
            WriteD(writer, 0);
            WriteH(writer, 60);
            WriteH(writer, 3700);
            WriteH(writer, 0);
            for (int slot = 0; slot < 15; slot++)
            {
                if (slot < 11)
                {
                    KeyValuePair<int, int> ability;
                    if (Player.Abilities.TryGetValue(slot, out ability))
                    {
                        WriteH(writer, ability.Key); // ability id
                        WriteH(writer, ability.Value); // ability level
                    }
                    else
                    {
                        WriteD(writer, 0);
                    }
                }
                else
                {
                    WriteD(writer, 0);
                }
            }
            WriteH(writer, 0);
            WriteH(writer, 65520);
            WriteH(writer, 7);
            WriteD(writer, 0);
        }
    }
}
