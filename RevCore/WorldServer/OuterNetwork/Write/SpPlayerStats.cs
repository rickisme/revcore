using Data.Structures.Player;
using System.Collections.Generic;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerStats : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerStats(Player p)
        {
            Player = p;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteH(writer, Player.Level);

            WriteH(writer, Player.GameStats.Spirit);
            WriteH(writer, Player.GameStats.Strength);
            WriteH(writer, Player.GameStats.Stamina);
            WriteH(writer, Player.GameStats.Dexterity);

            WriteH(writer, 0);

            WriteH(writer, Player.GameStats.Attack);
            WriteH(writer, Player.GameStats.Defense);
            WriteH(writer, Player.GameStats.Accuracy);
            WriteH(writer, Player.GameStats.Dodge);

            WriteH(writer, Player.AbilityPoint); // Ability point

            for (int slot = 0; slot < 15; slot++)
            {
                // ability id
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

            for (int slot = 0; slot < 32; slot++)
            {
                // skill id
                KeyValuePair<int, int> skill;
                if (Player.Skills.TryGetValue(slot, out skill))
                {
                    WriteD(writer, skill.Key); // ability id
                }
                else
                {
                    WriteD(writer, 0);
                }
            }

            for (int slot = 0; slot < 28; slot++)
            {
                // passive skill id
                KeyValuePair<int, int> passiveSkill;
                if (Player.PassiveSkills.TryGetValue(slot, out passiveSkill))
                {
                    WriteD(writer, passiveSkill.Key); // ability id
                }
                else
                {
                    WriteD(writer, 0);
                }
            }

            for (int slot = 0; slot < 16; slot++)
            {
                // ascension ability id
                KeyValuePair<int, int> ascensionability;
                if (Player.AscensionAbilities.TryGetValue(slot, out ascensionability))
                {
                    WriteD(writer, ascensionability.Key); // ascension ability id
                }
                else
                {
                    WriteD(writer, 0);
                }
            }

            for (int slot = 0; slot < 32; slot++)
            {
                // skill level
                KeyValuePair<int, int> skill;
                if (Player.Skills.TryGetValue(slot, out skill))
                {
                    WriteH(writer, skill.Value); // skill level
                }
                else
                {
                    WriteH(writer, 0);
                }
            }

            for (int slot = 0; slot < 28; slot++)
            {
                // passive skill level
                KeyValuePair<int, int> passiveSkill;
                if (Player.PassiveSkills.TryGetValue(slot, out passiveSkill))
                {
                    WriteH(writer, passiveSkill.Value); // passive skill level
                }
                else
                {
                    WriteH(writer, 0);
                }
            }

            for (int slot = 0; slot < 16; slot++)
            {
                // ascension ability level
                KeyValuePair<int, int> ascensionability;
                if (Player.AscensionAbilities.TryGetValue(slot, out ascensionability))
                {
                    WriteH(writer, ascensionability.Value); // ascension ability level
                }
                else
                {
                    WriteH(writer, 0);
                }
            }
            
            WriteH(writer, 0);
            WriteH(writer, 4);
            WriteH(writer, 3);
            WriteH(writer, 3);
            WriteH(writer, 3);
            WriteH(writer, 3);
            WriteH(writer, 4);
            WriteH(writer, 4);
            WriteH(writer, 4);
            WriteH(writer, 4);
            WriteH(writer, 4);
            WriteH(writer, 4);
            WriteH(writer, 0);
            WriteH(writer, 3);
            WriteH(writer, 4);
            WriteH(writer, 3);
            WriteH(writer, 3);
            WriteH(writer, 4);
            WriteH(writer, 0);
            WriteH(writer, 3);

            WriteH(writer, 0);
            WriteH(writer, 1);
            WriteH(writer, 1);
            WriteH(writer, 1);
            WriteH(writer, 0);
            WriteH(writer, 3);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            for (int slot = 0; slot < 5; slot++)
            {
                WriteD(writer, 0);
            }
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 3);
            WriteH(writer, 1);
            WriteH(writer, 1);
            WriteH(writer, 1);
            WriteH(writer, 1);
            WriteH(writer, 0);
            for (int slot = 0; slot < 16; slot++)
            {
                WriteD(writer, 0);
            }

            WriteH(writer, 0); // Defense Increase Rate
            WriteH(writer, 0);
            WriteD(writer, Player.HonorPoint); // Player_WuXun
            WriteD(writer, Player.KarmaPoint); // 人物善恶
            WriteD(writer, 1);
            WriteD(writer, 100);
            WriteH(writer, 1);

            for (int slot = 0; slot < 15; slot++)
            {
                // passive skill id
                KeyValuePair<int, int> ascensionSkill;
                if (Player.AscensionSkills.TryGetValue(slot, out ascensionSkill))
                {
                    WriteH(writer, ascensionSkill.Key); // skill id
                    WriteH(writer, ascensionSkill.Value); // skill level
                }
                else
                {
                    WriteD(writer, 0);
                }
            }

            WriteH(writer, 0);

            for (int slot = 0; slot < 18; slot++)
            {
                KeyValuePair<int, int> ascensionSkill;
                if (Player.AscensionSkills.TryGetValue(slot, out ascensionSkill))
                {
                    WriteD(writer, ascensionSkill.Key); // ascension skill id
                    WriteD(writer, ascensionSkill.Value); // ascension skill level
                }
                else
                {
                    WriteD(writer, 0);
                    WriteD(writer, 0);
                }
            }

            WriteD(writer, Player.AscensionPoint);

            for (int slot = 0; slot < 15; slot++)
            {
                if (slot < 11)
                {
                    KeyValuePair<int, int> ability;
                    if (Player.Abilities.TryGetValue(slot, out ability))
                    {
                        WriteC(writer, ability.Value); // ability level
                    }
                    else
                    {
                        WriteC(writer, 0);
                    }
                }
                else
                {
                    WriteC(writer, 0);
                }
            }

            for (int slot = 0; slot < 15; slot++)
            {
                KeyValuePair<int, int> ascensionSkill;
                if (Player.AscensionSkills.TryGetValue(slot, out ascensionSkill))
                {
                    WriteC(writer, ascensionSkill.Value); // skill level
                }
                else
                {
                    WriteC(writer, 0);
                }
            }

            WriteH(writer, 0);
        }
    }
}
