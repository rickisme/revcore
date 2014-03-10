using Data.Structures.Creature;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using System.Collections.Generic;
using Utilities;

namespace WorldServer.SkillEngine
{
    class SeUtils
    {
        public static int GetAttackDistance(Skill skill)
        {
            int result = 10;

            return result;
        }

        public static int CalculateDefaultAttackDamage(Creature creature, Creature target, int atk)
        {
            int damage = 0;
            int action = 112;

            creature.Attack.Results = new List<int>();
            for (int i = 0; i < 5; i++)
                creature.Attack.Results.Add(0);

            Player Player = creature as Player;
            if (Player != null)
            {
                //Log.Debug("CriticalAttackRate = {0}", Player.GameStats.CriticalAttackRate);
                if (Funcs.IsLuck(Player.GameStats.CriticalAttackRate))
                {
                    action = 134;
                    creature.Attack.AttackAction = action;
                    damage = (int)((((atk - (target.GameStats.Defense * 0.7)) * 1.5) + (Player.GameStats.Accuracy / 4)) * 2.0);
                    if (Player.IsRage) damage = (int)(damage * 1.3);
                    damage = Funcs.Random().Next(damage - 5, damage + 5);
                    creature.Attack.Results[0] = damage;
                    return damage;
                }

                if (Funcs.IsLuck(Player.GameStats.ComboAttackRate))
                {
                    action = Funcs.Random().Next(128, 129 + 1);
                    creature.Attack.AttackAction = action;
                    damage = (int)((((atk - (target.GameStats.Defense * 0.7)) * 1.5) + (Player.GameStats.Accuracy / 4)) * 3.0);
                    if (Player.IsRage) damage = (int)(damage * 1.3);
                    int dmg1 = Funcs.Random().Next(damage / 3 - 10, damage / 3 + 10);
                    int dmg2 = Funcs.Random().Next((damage - dmg1) / 2 - 10, (damage - dmg1) / 2 + 10);
                    int dmg3 = damage - dmg1 - dmg2;
                    creature.Attack.Results[0] = dmg1;
                    creature.Attack.Results[1] = dmg2;
                    creature.Attack.Results[2] = dmg3;

                    damage = dmg1 + dmg2 + dmg3;
                    return damage;
                }
                
                if (Player.Inventory.EquipItems[3].ItemId == 0)
                {
                    action = Funcs.Random().Next(112, 113 + 1);
                    creature.Attack.AttackAction = action;
                    damage = (int)((atk - (target.GameStats.Defense * 0.7)) * 1.5);
                    if (Player.IsRage) damage = (int)(damage * 1.3);
                    damage = Funcs.Random().Next(damage - 5, damage + 5);
                    creature.Attack.Results[0] = damage;
                    return damage;
                }
                else
                {
                    action = Funcs.Random().Next(126, 127 + 1);
                    creature.Attack.AttackAction = action;
                    damage = (int)((((atk - (target.GameStats.Defense * 0.7)) * 1.5) + (Player.GameStats.Accuracy / 4)));
                    if (Player.IsRage) damage = (int)(damage * 1.3);
                    damage = Funcs.Random().Next(damage - 5, damage + 5);
                    creature.Attack.Results[0] = damage;
                    return damage;
                }
            }
            else
            {
                action = Funcs.Random().Next(112, 113 + 1);
                creature.Attack.AttackAction = action;
                damage = (int)((atk - (target.GameStats.Defense * 0.7)) * 1.5);
                damage = Funcs.Random().Next(damage - 5, damage + 5);
                creature.Attack.Results[0] = damage;
                return damage;
            }
        }

        public static int CalculateSkillAttackDamage(Creature creature, Creature target, Skill skill)
        {
            int damage = 0;

            Player player = creature as Player;
            if(player != null)
            {
                switch (player.PlayerData.Class)
                {
                    case Data.Enums.PlayerClass.Blademan:

                        break;
                    case Data.Enums.PlayerClass.Swordman:

                        break;

                    default:

                        break;
                }
            }

            return damage;
        }

    }
}
