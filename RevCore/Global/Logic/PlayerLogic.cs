using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Player;
using Data.Structures.World;
using System.Collections.Generic;

namespace Global.Logic
{
    public class PlayerLogic : Global
    {
        public static void CheckName(IConnection connection, string name)
        {
            CheckNameResult result = PlayerService.CheckName(name);
            FeedbackService.SendCheckNameResult(connection, name, result);
        }

        public static void CreateCharacter(IConnection connection, PlayerData playerData)
        {
            if (connection.Account.Players.Count >= 5)
            {
                FeedbackService.SendCreateCharacterResult(connection, false);
            }

            Player player = PlayerService.CreateCharacter(connection, playerData);
            StorageService.AddStartItemsToPlayer(player);
            SkillsLearnService.AddStartPlayerAbility(player);

            FeedbackService.SendCreateCharacterResult(connection, true);
        }

        public static void PlayerEnterWorld(Player player)
        {
            PlayerService.PlayerEnterWorld(player);
            MapService.PlayerEnterWorld(player);
            
        }

        public static void PlayerEndGame(Player player)
        {
            if (player == null) return;

            PlayerService.PlayerEndGame(player);
            MapService.PlayerLeaveWorld(player);
            ControllerService.PlayerEndGame(player);

            //PartyService.UpdateParty(player.Party);

            //DuelService.PlayerLeaveWorld(player);
        }

        public static void InTheVision(Player player, Creature creature)
        {
            FeedbackService.SendCreatureInfo(player.Connection, creature);

            /*Npc npc = creature as Npc;
            if (npc != null)
                QuestEngine.ShowIcon(player, npc);*/
        }

        public static void OutOfVision(Player player, Creature creature)
        {
            ObserverService.RemoveObserved(player, creature);
            FeedbackService.SendRemoveCreature(player.Connection, creature);
        }

        public static void DistanceToCreatureRecalculated(Player player, Creature creature, double distance)
        {
            player.Ai.DistanceToCreatureRecalculated(creature, distance);
        }

        public static void ProcessChatMessage(IConnection connection, string playerName, string message, ChatType type)
        {
            if (AdminEngine.ProcessChatMessage(connection, message))
                return;

            ChatService.ProcessMessage(connection, playerName, message, type);
        }

        public static void PlayerMoved(Player player, float x1, float y1, float z1, float x2, float y2, float z2, float distance, int tagert)
        {
            PlayerService.PlayerMoved(player, x1, y1, z1, x2, y2, z2, distance, tagert);
            FeedbackService.PlayerMoved(player, x1, y1, z1, x2, y2, z2, distance, tagert);

            //PartyService.SendMemberPositionToPartyMembers(player);
        }

        public static void PleyerDied(Player player)
        {
            CreatureLogic.UpdateCreatureStats(player);
            FeedbackService.PlayerDied(player);
        }

        public static void LevelUp(Player player)
        {
            player.LifeStats.LevelUp();

            FeedbackService.PlayerLevelUp(player);
            StatsService.UpdateStats(player);
            //QuestEngine.PlayerLevelUp(player);
        }

        public static void UseSkill(IConnection connection, UseSkillArgs args)
        {
            SkillEngine.UseSkill(connection, args);
        }

        public static void MarkTarget(IConnection connection, Creature target)
        {
            SkillEngine.MarkTarget(connection, target);
        }

        public static void AttackTarget(IConnection connection, int TargetUid, int SkillId, float X, float Y, float Z)
        {
            Player Player = connection.Player;
            Player.Target = Global.VisibleService.FindTarget(Player, TargetUid);

            AiLogic.OnAttack(Player, Player.Target);
        }

        public static void RessurectByPremiumItem(Player player, long itemId)
        {
            PlayerService.RessurectByPremiumItem(player, itemId);
        }

        public static void Ressurect(Player player, int type)
        {
            ControllerService.TrySetDefaultController(player);

            switch (type)
            {
                case 99:
                    {
                        // Ressurect to bind point
                        Global.TeleportService.ForceTeleport(player, player.ClosestBindPoint);
                    }
                    break;

                case 100:
                    {
                        // Ressurect to current position
                        Global.TeleportService.ForceTeleport(player, player.Position);
                    }
                    break;
            }
        }

        public static void PlayerAction(Player player, int ActionId)
        {
            FeedbackService.PlayerAction(player, ActionId);
        }

        public static void PickUpItem(IConnection connection, Item item)
        {
            if (connection.Player == null)
                return;

            MapService.PickUpItem(connection.Player, item);
        }

        public static void LevelUpAbility(Player player, Dictionary<int, KeyValuePair<int, int>> AbilityList, int AbilityId, int AbilityPoint)
        {
            SkillsLearnService.LevelUpAbility(player, AbilityId, AbilityPoint);
        }

        public static void OptionSetting(Player player, Settings settings)
        {
            PlayerService.UpdateClientSetting(player, settings);
        }

        public static void PlayerLearnSkill(Player player, int skillid)
        {
            SkillsLearnService.LearnSkill(player, skillid);
            StatsService.UpdateStats(player);
            FeedbackService.PlayerLearnSkill(player);
        }
    }
}
