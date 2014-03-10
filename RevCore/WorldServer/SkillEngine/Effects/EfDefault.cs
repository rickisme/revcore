using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.SkillEngine;
using Utilities;

namespace WorldServer.SkillEngine.Effects
{
    abstract class EfDefault : IEffect
    {
        public Creature Creature;

        public Ability Ability;

        public bool IsUpdateStats = false;

        public long LastTick = Funcs.GetCurrentMilliseconds();

        public virtual void Init()
        {
            
        }

        public virtual void Tick()
        {

        }

        public virtual void UpdateStats()
        {

        }

        public virtual void Action()
        {
            long now = Funcs.GetCurrentMilliseconds();
            long nextTick = LastTick + 1000;

            if (nextTick < now)
            {
                LastTick = nextTick;
                Tick();
            }
        }

        public virtual void SetImpact(Data.Structures.Creature.CreatureEffectsImpact impact)
        {
            
        }

        public virtual void Release()
        {
            Creature = null;
            Ability = null;
        }


        public void SetActive(bool active)
        {
            IsUpdateStats = active;
        }
    }
}
