using Data.Structures.Creature;
using Data.Structures.Player;

namespace Global.Interfaces
{
    public interface IObserverService : IComponent
    {
        void AddObserved(Player player, Creature creature);
        void RemoveObserved(Player Player, Creature creature);
        void NotifyHpMpSpChanged(Creature creature);
    }
}
