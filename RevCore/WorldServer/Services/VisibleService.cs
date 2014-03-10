using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.World;
using Global.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using WorldServer.Extensions;

namespace WorldServer.Services
{
    class VisibleService : IVisibleService
    {
        public void Send(Creature creature, ISendPacket packet)
        {
            Player player = creature as Player;
            if (player != null)
            {
                if (player.Connection != null)
                    packet.Send(player.Connection);
            }

            creature.VisiblePlayers.Each(p => packet.Send(p.Connection));
        }

        public List<Creature> FindTargets(Creature creature, Point3D position, double distance)
        {
            return FindTargets(creature, position.X, position.Y, position.Z, distance);
        }

        public List<Creature> FindTargets(Creature creature, WorldPosition position, double distance)
        {
            return FindTargets(creature, position.X, position.Y, position.Z, distance);
        }

        public List<Creature> FindTargets(Creature creature, float x, float y, float z, double distance)
        {
            List<Creature> finded = new List<Creature>();

            

            return finded;
        }

        public List<Player> FindPlayers(Creature creature, float x, float y, float z, double distance)
        {
            distance += 45;
            double verticalDistance = distance * 2;

            if (!(creature is Player))
                return
                    creature.VisiblePlayers.Select(
                        player => player.Position.DistanceTo(x, y) <= distance &&
                                  Math.Abs(z - player.Position.Z) < verticalDistance);
            return
                creature.VisiblePlayers.Select(
                    player =>
                    (player.Position.DistanceTo(x, y) <= distance &&
                     Math.Abs(z - player.Position.Z) < verticalDistance/* &&
                     ((Player)creature).Duel != null &&
                     ((Player)creature).Duel.Equals(player.Duel)*/));
        }

        public List<Npc> FindNpcs(Creature creature, float x, float y, float z, double distance, bool findVillagers = false)
        {
            double verticalDistance = distance * 2;

            return creature.VisibleNpcs.Select(npc =>
                                               npc.Position.DistanceTo(x, y) <= distance
                                               && Math.Abs(z - npc.Position.Z) < verticalDistance
                                               /*&& (!npc.NpcTemplate.IsVillager || findVillagers)*/);
        }

        public void Action()
        {

        }


        public Creature FindTarget(Player player, int uid)
        {
            return player.Instance.Npcs.Where(v => v.UID == uid).ToList().FirstOrDefault();
        }

        public Player FindTarget(Player player, string uname)
        {
            return player.Instance.Players.Where(p => p.PlayerData.Name == uname).ToList().FirstOrDefault();
        }
    }
}
