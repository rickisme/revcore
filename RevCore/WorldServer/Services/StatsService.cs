using Data.Enums;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Data.Structures.Template.Item.CategorieStats;
using Global.Interfaces;
using System.Collections.Generic;
using Utilities;
using WorldServer.OuterNetwork.Write;
using WorldServer.SkillEngine;

namespace WorldServer.Services
{
    class StatsService : IStatsService
    {
        public static Dictionary<PlayerClass, Dictionary<int, CreatureBaseStats>> PlayerStats = new Dictionary<PlayerClass, Dictionary<int, CreatureBaseStats>>();

        public static int MaxLevel = 130;

        public void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                PlayerStats.Add((PlayerClass)i+1, new Dictionary<int, CreatureBaseStats>());

                CreatureBaseStats firstLevelStats = Data.Data.Stats[i];

                for (int j = 1; j < MaxLevel; j++)
                {
                    CreatureBaseStats stats = firstLevelStats.Clone();

                    switch (stats.PlayerClass)
                    {
                        case PlayerClass.Blademan:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j;
                            stats.Strength += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Stamina += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Dexterity += j;
                            stats.Attack += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case PlayerClass.Swordman:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Strength += j;
                            stats.Stamina += (j * 2);
                            stats.Dexterity += (j * 2);
                            stats.Attack += (j * 2);
                            stats.Defense += j;
                            stats.Accuracy += (j * 2);
                            stats.Dodge += (j * 2);
                            break;
                        case PlayerClass.Spearman:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j;
                            stats.Strength += j;
                            stats.Stamina += (j * 3);
                            stats.Dexterity += j;
                            stats.Attack += (j * 3);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case PlayerClass.Bowman:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += (j * 2);
                            stats.Strength += j;
                            stats.Stamina += j + ((j % 2 == 0) ? 2 : 3);
                            stats.Dexterity += (j * 3);
                            stats.Attack += j + ((j % 2 == 0) ? 2 : 3);
                            stats.Defense += j;
                            stats.Accuracy += (j * 3);
                            stats.Dodge += (j * 3);
                            break;
                        case PlayerClass.Medic:
                            stats.HpBase += (j * 7);
                            stats.MpBase += (j * 6);
                            stats.Spirit += (j * 3);
                            stats.Strength += (j * 2);
                            stats.Stamina += (j * 2);
                            stats.Dexterity += j;
                            stats.Attack += (j * 2);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case PlayerClass.Ninja:
                            stats.HpBase += (j * 10);
                            stats.MpBase += (j * 4);
                            stats.Spirit += (j * 2);
                            stats.Strength += (j * 2);
                            stats.Stamina += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Dexterity += (j * 3);
                            stats.Attack += j + ((j % 2 == 0) ? 2 : 3);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += (j * 2);
                            break;
                        case PlayerClass.Busker: // Temp copy from medic
                            stats.HpBase += (j * 7);
                            stats.MpBase += (j * 6);
                            stats.Spirit += (j * 3);
                            stats.Strength += (j * 2);
                            stats.Stamina += (j * 2);
                            stats.Dexterity += j;
                            stats.Attack += (j * 2);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case PlayerClass.Hanbi:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j;
                            stats.Strength += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Stamina += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Dexterity += j;
                            stats.Attack += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Defense += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Accuracy += j + ((j % 4 == 0) ? 1 : 2);
                            stats.Dodge += j + ((j % 2 == 0) ? 1 : 2);
                            break;
                    }

                    stats.SpBase += (j * 10);

                    PlayerStats[(PlayerClass)i+1].Add(j, stats);
                }
            }


        }

        public CreatureBaseStats InitStats(Data.Structures.Creature.Creature creature)
        {
            Player player = creature as Player;
            if (player != null)
                return GetBaseStats(player).Clone();

            Npc npc = creature as Npc;
            if (npc != null)
                return GetNpcStats(npc);

            Log.Error("StatsService: Unknown type: {0}.", creature.GetType().Name);
            return new CreatureBaseStats();
        }

        public CreatureBaseStats GetBaseStats(Data.Structures.Player.Player player)
        {
            return PlayerStats[player.PlayerData.Class][player.GetLevel()];
        }

        private CreatureBaseStats GetNpcStats(Npc npc)
        {
            return new CreatureBaseStats()
            {
                HpBase = npc.NpcTemplate.HealthPoint,
                Attack = npc.NpcTemplate.Attack,
                Defense = npc.NpcTemplate.Defense,
                Exp = npc.NpcTemplate.Exp,
            };
        }

        public void UpdateStats(Creature creature)
        {
            Player player = creature as Player;
            if (player != null)
            {
                UpdatePlayerStats(player);
                return;
            }

            Npc npc = creature as Npc;
            if (npc != null)
            {
                UpdateNpcStats(npc);
                return;
            }

            Log.Error("StatsService: Unknown type: {0}.", creature.GetType().Name);
        }

        private void UpdatePlayerStats(Player player)
        {
            CreatureBaseStats baseStats = GetBaseStats(player);
            baseStats.CopyTo(player.GameStats);

            int itemsAttack = 0,
                itemsDefense = 0;

            lock (player.Inventory.ItemsLock)
            {
                foreach(var item in player.Inventory.EquipItems.Values)
                {
                    if (item == null)
                        continue;

                    ItemTemplate itemTemplate = item.ItemTemplate;

                    if (itemTemplate != null)
                    {
                        itemsAttack += itemTemplate.MinAttack + ((itemTemplate.Category == 3) ? (item.Upgrade * 5) : 0);
                        itemsDefense += itemTemplate.Defense + ((itemTemplate.Category == 1) ? (item.Upgrade * 5) : 0);
                    }
                }
            }

            player.GameStats.Attack = (int)(baseStats.Attack + (0.03f * baseStats.Strength + 3) + itemsAttack);
            player.GameStats.Defense = (int)(baseStats.Defense + (0.01f * baseStats.Stamina + 0.5) + itemsDefense);

            player.EffectsImpact.ResetChanges(player);
            player.EffectsImpact.ApplyChanges(player.GameStats);

            new SpPlayerStats(player).Send(player);
            new SpPlayerHpMpSp(player).Send(player);
        }

        private void UpdateNpcStats(Npc npc)
        {
            npc.EffectsImpact.ResetChanges(npc);
            npc.EffectsImpact.ApplyChanges(npc.GameStats);
        }

        public void Action()
        {
            
        }
    }
}
