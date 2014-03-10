using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.World;

namespace Global.Interfaces
{
    public interface IMapService : IComponent
    {
        void Init();
        void PlayerEnterWorld(Player player);
        void PlayerLeaveWorld(Player player);
        void CreateDrop(Npc npc, Player player);
        void PickUpItem(Player player, Item item);
        bool IsDungeon(int mapId);
    }
}
