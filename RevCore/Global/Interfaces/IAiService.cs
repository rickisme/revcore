using Data.Interfaces;
using Data.Structures.Creature;

namespace Global.Interfaces
{
    public interface IAiService : IComponent
    {
        IAi CreateAi(Creature creature);
    }
}
