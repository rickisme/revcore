using Data.Structures.Creature;
using Global.Logic;

namespace WorldServer.SkillEngine.Effects
{
    class EfLethalBlow : EfDefault
    {
        public override void Init()
        {
            CreatureLogic.UpdateCreatureStats(Creature);
        }

        public override void SetImpact(CreatureEffectsImpact impact)
        {
            if (IsUpdateStats)
                return;

            double increase = 0.0;

            if (Ability.level < 1)
                return;

            increase = Ability.FirstLevel;

            for (int i = 1; i < Ability.level; i++)
            {
                increase += Ability.Step;
            }

            impact.ChangeOfCriticalAttackRate = (int)increase;
            IsUpdateStats = true;
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
