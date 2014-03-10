using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.World;
using DatabaseFactory;
using Global.Interfaces;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Utilities;
using WorldServer.Config;
using WorldServer.Extensions;
using WorldServer.OuterNetwork;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Services
{
    public class PlayerService : IPlayerService
    {
        public static List<Player> PlayersOnline = new List<Player>();

        public void Action()
        {
            for (int i = 0; i < PlayersOnline.Count; i++)
            {
                try
                {
                    if (PlayersOnline[i].Ai != null)
                        PlayersOnline[i].Ai.Action();

                    if (PlayersOnline[i].Visible != null)
                        PlayersOnline[i].Visible.Update();

                    if (PlayersOnline[i].Controller != null)
                        PlayersOnline[i].Controller.Action();
                }
                catch(Exception ex)
                {
                    //Collection modified
                    Log.ErrorException("PlayerService.Action:", ex);
                }

                if ((i & 511) == 0) // 2^N - 1
                    Thread.Sleep(1);
            }
        }

        public void Send(ISendPacket packet)
        {
            PlayersOnline.Each(player => packet.Send(player.Connection));
        }

        public void InitPlayer(Player player)
        {
            player.GameStats = CreatureLogic.InitGameStats(player);
            CreatureLogic.UpdateCreatureStats(player);

            AiLogic.InitAi(player);

            PlayersOnline.Add(player);
        }

        public CheckNameResult CheckName(string name)
        {
            return DataBasePlayer.CheckName(name);
        }


        public Player CreateCharacter(IConnection connection, PlayerData playerData)
        {
            Player player = new Player
            {
                PlayerData = playerData,
                Account = connection.Account,
                Position = new WorldPosition
                {
                    MapId = 101,
                    X = 300.0F,
                    Y = 1865.0F,
                    Z = 15.0F,
                },
            };

            player.ServerId = Properties.Settings.Default.SERVER_ID;
            player.GameStats = Global.Global.StatsService.InitStats(player);

            player.PlayerId = DataBasePlayer.SavePlayer(player, true);
            connection.Account.Players.Add(player);
            return player;
        }

        public bool IsPlayerOnline(Player player)
        {
            return PlayersOnline.Contains(player);
        }


        public void PlayerEnterWorld(Player player)
        {
            Global.Global.PlayerService.InitPlayer(player);

            new SpServerTime(0).Send(player);
            new SpPlayerRunning(true).Send(player);
            //new SpPlayerSkillNormal(player).Send(player);
            new SpInventoryQuest(player).Send(player);
            
            Global.Global.StorageService.ShowPlayerStorage(player, StorageType.Inventory);

            new SpPlayerHpMpSp(player).Send(player);
            new SpPlayerQuickInfo(player).Send(player);
            new SpWeightMoney(player).Send(player);
            new SpPlayerDPoint(player).Send(player);
            new SpPlayerInfo(player).Send(player);

            if (player.PrivateShop != null)
            {
                new SpPrivateShopInfo(player).Send(player);
            }

            SkillEngine.SkillEngine.AbilityProcessor.ApplyAbility(player);
            Global.Global.StatsService.UpdateStats(player);

            new SpPlayerExpAndPointUp(player).Send(player);

            new SpQuestList(player).Send(player);

            new SpBindPoint(player).Send(player);

            //SystemMessages.EnterGameMessage.Send(player);
            Global.Global.ScriptEngine.SendWelComeMessage(player);
        }

        public void PlayerEndGame(Player player)
        {
            if (player.Ai != null)
            {
                player.Ai.Release();
                player.Ai = null;
            }

            AccountService.IdFactory.Release(player.Account.SessionID);

            DataBasePlayer.SavePlayer(player);
            DataBaseStorage.SavePlayerStorage(player.PlayerId, player.Inventory);
            DataBaseAbility.SavePlayerAbility(player, SkillType.Basic);
            DataBaseAbility.SavePlayerAbility(player, SkillType.Ascension);

            DataBaseSkill.SavePlayerSkill(player, SkillType.Basic);
            DataBaseSkill.SavePlayerSkill(player, SkillType.Ascension);
            DataBaseSkill.SavePlayerSkill(player, SkillType.Passive);

            DataBaseQuest.SavePlayerQuest(player);

            PlayersOnline.Remove(player);
        }

        public Player GetPlayerByName(string playerName)
        {
            try
            {
                return PlayersOnline.First(player => player.PlayerData.Name == playerName);
            }
            catch
            {

            }
            return null;
        }

        public void PlayerMoved(Player player, float x1, float y1, float z1, float x2, float y2, float z2, float distance, int target)
        {
            if (player.PlayerMode != PlayerMode.Normal)
                player.PlayerMode = PlayerMode.Normal;

            Creature Target = player
                                .Instance
                                .Npcs
                                .Where(npc => npc.UID == target)
                                .FirstOrDefault();

            player.Target = Target;

            player.Position.X = x1;
            player.Position.Y = y1;

            player.Position.X2 = x2;
            player.Position.Y2 = y2;

            if (player.Instance != null)
                player.Instance.OnMove(player);
        }

        public void AddExp(Player player, long value, Npc npc = null)
        {
            value *= Rate.EXP;

            //todo rate modifiers
            if (player.GetLevel() >= Rate.LEVEL_CAP)
            {
                //new SpSystemNotice("Sorry, but now, level cap is " + GamePlay.Default.LevelCap).Send(player);
                return;
            }

            int val1 = npc.NpcTemplate.Exp * Rate.KI_EXP;
            int val2 = val1 / 3;
            int ki = Funcs.Random().Next(val1 - val2, val1 + val2);
            player.SkillPoint += ki;

            SetExp(player, player.Exp + value, npc);
        }

        public void SetExp(Player player, long add, Npc npc)
        {

            int maxLevel = Data.Data.PlayerExperience.Count - 1;

            long maxExp = Data.Data.PlayerExperience[maxLevel - 1];
            int level = 1;
            

            if (add > maxExp)
                add = maxExp;

            while ((level + 1) != maxLevel && add >= Data.Data.PlayerExperience[level])
            {
                level++;
            }

            long added = add - player.Exp;

            if (level != player.Level)
            {
                player.Level = level;
                player.Exp = add;
                player.AbilityPoint += 1;
                PlayerLogic.LevelUp(player);
            }
            else
                player.Exp = add;

            new SpPlayerExpAndPointUp(player, added, npc).Send(player.Connection);
        }


        public void RessurectByPremiumItem(Player player, long itemId)
        {
            StorageItem item = player.Inventory.GetItemById(itemId);

            if (item == null)
            {
                new SpPremiumRevive(false, itemId).Send(player);
                return;
            }
        }


        public void UpdateClientSetting(Player player, Settings Settings)
        {
            player.Settings = Settings;
            Global.Global.VisibleService.Send(player, new SpPlayerInfo(player));
            Global.Global.VisibleService.Send(player, new SpEquipInfo(player));
        }
    }
}
