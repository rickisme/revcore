using Data.Enums;
using Data.Structures;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Template;
using Data.Structures.World;
using Global.Interfaces;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Threading;
using Utilities;
using WorldServer.Config;
using WorldServer.DungeonEngine.Dungeons;
using WorldServer.Extensions;
using WorldServer.OuterNetwork;
using WorldServer.OuterNetwork.Write;
using WorldServer.Structures;

namespace WorldServer.Services
{
    public class MapService : IMapService
    {
        public static Dictionary<int, List<MapInstance>> Maps = new Dictionary<int, List<MapInstance>>();
        public static object MapLock = new object();

        public static Dictionary<int, Type> Dungeons = new Dictionary<int, Type>();

        protected static Dictionary<int, List<SpawnTemplate>> MergedSpawn = new Dictionary<int, List<SpawnTemplate>>();

        public void Init()
        {
            List<int> fullIds = new List<int>();

            foreach (KeyValuePair<int, List<SpawnTemplate>> keyValuePair in Data.Data.Spawns)
            {
                for (int i = 0; i < keyValuePair.Value.Count; i++)
                {
                    if (keyValuePair.Value[i].Type == 1023)
                    {
                        keyValuePair.Value.RemoveAt(i);
                        i--;
                    }
                }

                MergedSpawn.Add(keyValuePair.Key, keyValuePair.Value);

                foreach (SpawnTemplate spawnTemplate in keyValuePair.Value)
                    if (!fullIds.Contains(spawnTemplate.NpcId))
                        fullIds.Add(spawnTemplate.NpcId);
            }
        }

        public void Action()
        {
            try
            {
                lock (MapLock)
                {
                    foreach (var map in Maps.Values)
                    {
                        for (int i = 0; i < map.Count; i++)
                        {
                            for (int j = 0; j < map[i].Npcs.Count; j++)
                            {
                                try
                                {
                                    map[i].Npcs[j].Ai.Action();
                                }
                                catch(Exception e)
                                {
                                    Log.ErrorException("Ai.Action:", e);
                                }

                                if ((j & 1023) == 0) // 2^N - 1
                                    Thread.Sleep(1);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }


        public void PlayerEnterWorld(Player player)
        {
            if (!Maps.ContainsKey(player.Position.MapId))
                lock (MapLock)
                    Maps.Add(player.Position.MapId, new List<MapInstance>());

            var instance = GetMapInstance(player, player.Position.MapId);

            if (!Maps[player.Position.MapId].Contains(instance))
                Maps[player.Position.MapId].Add(instance);

            SpawnRxjhObject(player, instance);

            if (player.Visible != null)
            {
                player.Visible.Stop();
                player.Visible.Release();
                player.Visible = null;
            }

            player.Visible = new Visible { Player = player };
            player.Visible.Start();
        }

        public void PlayerLeaveWorld(Player player)
        {
            if (player.Instance != null)
            {
                if (player.Instance.Players.Count <= 1 && IsDungeon(player.Instance.MapId))
                    DestructInstance(player.Instance);
                else
                    DespawnRxjhObject(player);

                player.Instance = null;

                player.Visible.Stop();
                player.Visible.Release();
                player.Visible = null;
            }
        }

        public void SpawnMap(MapInstance instance)
        {
            if (MergedSpawn.ContainsKey(instance.MapId))
            {
                foreach (SpawnTemplate template in MergedSpawn[instance.MapId])
                {
                    if (template.GroupSpawn)
                    {
                        for (int i = 0; i < template.Count; i++)
                        {
                            Npc npc = CreateNpc(template);
                            Random rnd = new Random((int)(DateTime.Now.Ticks + Funcs.Random().Next(int.MinValue, (int)Math.Abs(npc.Position.X)))); ;
                            npc.Position.X += rnd.Next(0, 400) * (rnd.Next(0, 100) < 50 ? 1 : -1);
                            npc.Position.Y += rnd.Next(0, 400) * (rnd.Next(0, 100) < 50 ? 1 : -1);
                            npc.Position.CopyTo(npc.BindPoint);
                            SpawnRxjhObject(npc, instance);
                        }
                    }
                    else
                        SpawnRxjhObject(CreateNpc(template), instance);
                }
            }

            if (!Maps.ContainsKey(instance.MapId))
                Maps.Add(instance.MapId, new List<MapInstance>());

            Maps[instance.MapId].Add(instance);

        }

        public void SpawnRxjhObject(RxjhObject obj, MapInstance instance)
        {
            var creature = obj as Creature;
            if (creature != null)
            {
                lock (instance.CreaturesLock)
                {
                    if (obj is Npc)
                        instance.AddNpc((Npc)obj);
                    else if (obj is Player)
                        instance.Players.Add((Player)obj);
                    else if (obj is Item)
                        instance.Items.Add((Item)obj);
                }

                creature.Instance = instance;
            }
        }

        public void DespawnRxjhObject(RxjhObject obj)
        {
            var creature = obj as Creature;
            if (creature != null)
            {
                lock (creature.Instance.CreaturesLock)
                {
                    if (creature is Npc)
                    {
                        creature.Instance.Npcs.Remove((Npc)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleNpcs.Remove((Npc)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Player)
                    {
                        creature.Instance.Players.Remove((Player)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisiblePlayers.Remove((Player)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                    else if (creature is Item)
                    {
                        creature.Instance.Items.Remove((Item)obj);
                        creature.VisiblePlayers.Each(player =>
                        {
                            player.VisibleItems.Remove((Item)obj);
                            PlayerLogic.OutOfVision(player, creature);
                        });
                    }
                }

                if (!(creature is Player))
                    creature.Release();
            }
        }

        private void DestructInstance(MapInstance instance)
        {
            instance.Release();
            Maps[instance.MapId].Remove(instance);
        }

        public static Npc CreateNpc(SpawnTemplate spawnTemplate)
        {
            NpcTemplate npcTemplate = (Data.Data.NpcTemplates.ContainsKey(spawnTemplate.NpcId))
                ? Data.Data.NpcTemplates[spawnTemplate.NpcId]
                : new NpcTemplate();

            var npc = new Npc
            {
                NpcId = spawnTemplate.NpcId,
                SpawnTemplate = spawnTemplate,
                NpcTemplate = npcTemplate,

                Position = new WorldPosition
                {
                    MapId = spawnTemplate.MapId,
                    X = spawnTemplate.X,
                    Y = spawnTemplate.Y,
                    Z = spawnTemplate.Z,
                },
            };

            npc.BindPoint = npc.Position.Clone();

            npc.GameStats = CreatureLogic.InitGameStats(npc);
            CreatureLogic.UpdateCreatureStats(npc);

            AiLogic.InitAi(npc);

            return npc;
        }

        private MapInstance GetMapInstance(Player player, int mapId)
        {
            if (IsDungeon(mapId))
            {
                if (player.Party != null)
                    lock (player.Party.MemberLock)
                        foreach (Player partyMember in player.Party.PartyMembers)
                            if (Global.Global.PlayerService.IsPlayerOnline(partyMember) && partyMember.Instance != null &&
                                partyMember.Instance.MapId == mapId)
                                return partyMember.Instance;

                var dungeon = ((ADungeon)Activator.CreateInstance(Dungeons[mapId]));
                dungeon.MapId = mapId;

                SpawnMap(dungeon);

                dungeon.Init();
                return dungeon;
            }

            if (Maps[mapId].Count > 0)
                return Maps[mapId][0];

            var ins = new MapInstance { MapId = mapId };
            SpawnMap(ins);

            return ins;
        }

        public bool IsDungeon(int mapId)
        {
            return Dungeons.ContainsKey(mapId);
        }


        public void CreateDrop(Npc npc, Player player)
        {
            if (Funcs.IsLuck(75))
            {
                var money = (int)(10 * npc.NpcTemplate.Exp / Funcs.Random().Next(20, 60));
                var item = new Item
                        {
                            Owner = player,
                            Npc = npc,

                            ItemId = 2000000000,
                            Count = money,
                            Position = Geom.RandomCirclePosition(npc.Position, Funcs.Random().Next(5, 10)),
                            Instance = player.Instance,
                        };

                if (Custom.MONEY_DROP_STYLE == 1)
                {
                    player.Inventory.Money += money;
                    new SpItemPickupMsg(ItemPickUp.GotMoney, item).Send(player);
                }
                else
                {
                    player.Instance.AddDrop(item);
                }
            }

            if (Funcs.IsLuck(30))
                return;

            if (!Data.Data.Drop.ContainsKey(npc.NpcTemplate.ID))
                return;

            List<long> items = Data.Data.Drop[npc.NpcTemplate.ID];

            if (items == null)
                return;

            int count = 0;
            int rate = Funcs.Random().Next(0, 2500);

            if (rate < 10)
                count = 6;
            else if (rate < 30)
                count = 5;
            else if (rate < 90)
                count = 4;
            else if (rate < 270)
                count = 3;
            else if (rate < 600)
                count = 2;
            else if (rate < 1800)
                count = 1;

            if (items.Count < count)
                count = items.Count;

            for (int i = 0; i < count; i++)
            {
                long itemId = items[Funcs.Random().Next(0, items.Count)];

                if (!Data.Data.ItemTemplates.ContainsKey(itemId))
                    continue;

                player.Instance.AddDrop(
                    new Item
                    {
                        Owner = player,
                        Npc = npc,

                        ItemId = itemId,
                        Count = 1,
                        Position = Geom.RandomCirclePosition(npc.Position, Funcs.Random().Next(5, 10)),
                        Instance = player.Instance,
                    });
            }
        }


        public void PickUpItem(Player player, Item item)
        {
            if (item.Owner.Party == null && item.Owner != player)
            {
                SystemMessages.YouCantPickUpItem.Send(player);
                return;
            }
            if (item.Owner.Party != null && !item.Owner.Party.PartyMembers.Contains(player))
            {
                SystemMessages.YouCantPickUpItem.Send(player.Connection);
                return;
            }

            if (item.ItemId == 2000000000)
            {
                if ((player.Inventory.Money + item.Count) >= 10000000000)
                {
                    new SpItemPickupMsg(ItemPickUp.MaxMoney, item).Send(player);
                    return;
                }

                Global.Global.StorageService.AddMoneys(player, player.Inventory, item.Count);
            }
            else
            {
                var template = Data.Data.ItemTemplates[item.ItemId];

                if ((Global.Global.StorageService.GetTotalWeight(player, player.Inventory) + template.Weight) > player.Inventory.MaxWeight)
                {
                    new SpItemPickupMsg(ItemPickUp.MaxWeight, item).Send(player);
                    return;
                }

                if (!Global.Global.StorageService.AddItem(player, player.Inventory, new StorageItem { ItemId = item.ItemId, Amount = item.Count }))
                {
                    new SpItemPickupMsg(ItemPickUp.InventoryIsFull, item).Send(player);
                    return;
                }
            }

            player.Instance.RemoveItem(item);
        }
    }
}
