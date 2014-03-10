using Data.Structures.Player;
using Data.Structures.World;

namespace Global.Interfaces
{
    public interface ITeleportService : IComponent
    {
        void ForceTeleport(Player player, WorldPosition position);
        WorldPosition GetBindPoint(Player player);
    }
}
