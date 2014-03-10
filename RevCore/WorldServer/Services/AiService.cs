using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Global.Interfaces;
using WorldServer.AiEngine;

namespace WorldServer.Services
{
    class AiService : IAiService
    {
        public IAi CreateAi(Creature creature)
        {
            if (creature is Player)
                return new PlayerAi();

            if (creature is Npc)
                return new NpcAi();

            return new DefaultAi();
        }

        public void Action()
        {

        }
    }
}
