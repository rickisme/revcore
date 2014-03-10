using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Geometry;
using Data.Structures.Player;
using Data.Structures.World;
using System.Collections.Generic;

namespace Global.Interfaces
{
    public interface IVisibleService : IComponent
    {
        void Send(Creature creature, ISendPacket packet);
        Creature FindTarget(Player player, int uid);

        Player FindTarget(Player player, string uname);
        List<Creature> FindTargets(Creature creature, Point3D position, double distance);
        List<Creature> FindTargets(Creature creature, WorldPosition position, double distance);
    }
}
