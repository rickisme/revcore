using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.World;
using Global.Interfaces;
using System;
using System.Collections.Generic;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Services
{
    public class FeedbackService : IFeedbackService
    {
        public void Action()
        {

        }

        public void OnAuthorized(IConnection connection)
        {
            new SpAuth().Send(connection);
        }

        public void SendPlayerLists(IConnection connection)
        {
            if (connection.Account.Players.Count > 0)
            {
                for (int i = 0; i < connection.Account.Players.Count; i++)
                {
                    new SpPlayerList(i, connection.Account.Players[i], CharacterListResponse.Exists).Send(connection);
                }
            }
            else
                new SpPlayerList(0, null, CharacterListResponse.None).Send(connection);
        }

        public void SendCheckNameResult(IConnection connection, string name, CheckNameResult result)
        {
            new SpCheckName(name, result).Send(connection);
        }

        public void SendCreateCharacterResult(IConnection connection, bool result)
        {
            new SpCreatePlayer(result).Send(connection);
        }


        public void SendCreatureInfo(IConnection connection, Creature creature)
        {
            var player = creature as Player;
            if (player != null)
            {
                try
                {
                    new SpPlayerInfo(player).Send(connection);
                }
                catch (Exception e)
                {
                    Log.Error("Exception " + e);
                }

                return;
            }

            var npc = creature as Npc;
            if (npc != null)
            {
                try
                {
                    new SpNpcSpawn(npc).Send(connection);
                }
                catch (Exception e)
                {
                    Log.Error("Exception " + e);
                }

                return;
            }

            var item = creature as Item;
            if (item != null)
            {
                new SpDropInfo(item).Send(connection);
                return;
            }
        }

        public void SendRemoveCreature(IConnection connection, Creature creature)
        {
            var player = creature as Player;
            if (player != null)
            {
                new SpPlayerRemove(player).Send(connection);
                return;
            }

            var npc = creature as Npc;
            if (npc != null)
            {
                new SpNpcDespawn(npc).Send(connection);
            }

            var item = creature as Item;
            if (item != null)
            {
                new SpDropRemove(item).Send(connection);
            }
        }

        public void PlayerMoved(Player player, float x1, float y1, float z1, float x2, float y2, float z2, float distance, int tagert)
        {
            Global.Global.VisibleService.Send(player, new SpPlayerMove(player, x1, y1, z1, x2, y2, z2, distance, tagert));
        }

        public void HpMpSpChanged(Player player)
        {
            Global.Global.VisibleService.Send(player, new SpPlayerHpMpSp(player));
        }

        public void PlayerDied(Player player)
        {
            WorldPosition bindPoint = GetNearestBindPoint(player);
            player.ClosestBindPoint = bindPoint;

            Global.Global.VisibleService.Send(player, new SpCreatureDied(player));
        }

        private WorldPosition GetNearestBindPoint(Player player)
        {
            WorldPosition position = player.Position;
            var safeHeavens = new List<WorldPosition>();
            if (Data.Data.BindPoints.ContainsKey(position.MapId))
                safeHeavens.AddRange(Data.Data.BindPoints[position.MapId]);
            else
                foreach (var bindPoint in Data.Data.BindPoints)
                    safeHeavens.AddRange(bindPoint.Value);

            double min = double.MaxValue;
            WorldPosition nearest = null;
            foreach (var heaven in safeHeavens)
            {
                double distance = heaven.DistanceTo(position);
                if (distance > min)
                    continue;

                min = distance;
                nearest = heaven;
            }

            return nearest;
        }


        public void AttackStageEnd(Creature creature)
        {
            //Global.Global.VisibleService.Send(creature, new SpAttack(creature, creature.Attack));
            /*Player player = creature as Player;
            if (player != null)
                Global.Global.SkillEngine.UseSkill((creature as Player).Connection, creature.Attack.Args);

            Npc npc = creature as Npc;
            if (npc != null)
                Global.Global.SkillEngine.UseSkill(npc, null);*/
        }

        public void AttackFinished(Creature creature)
        {
            // Send Attack result
        }


        public void StatsUpdated(Player player)
        {
            new SpPlayerStats(player).Send(player.Connection);
        }


        public void Exit(IConnection connection)
        {
            new SpExit().Send(connection);
        }

        public void Logout(IConnection connection) 
        {
            new SpPlayerLogout().Send(connection);
        }

        public void PlayerAction(Player player, int ActionId)
        {
            Global.Global.VisibleService.Send(player, new SpPlayerAction(player, ActionId));
        }


        public void PlayerLevelUp(Player player)
        {
            Global.Global.VisibleService.Send(player, new SpPlayerLevelUp(player));
            Global.Global.VisibleService.Send(player, new SpPlayerHpMpSp(player));
        }


        public void PlayerLearnSkill(Player player)
        {
            new SpLearnSkillMessage(player).Send(player);
            new SpPlayerExpAndPointUp(player).Send(player);
        }
    }
}
