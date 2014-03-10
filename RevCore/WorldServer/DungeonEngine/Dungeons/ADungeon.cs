using Data.Structures.World;

namespace WorldServer.DungeonEngine.Dungeons
{
    abstract class ADungeon : MapInstance
    {
        public int ParentMapId = 0;

        public long EnterTime = 0;

        public abstract void Init();
    }
}
