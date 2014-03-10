using Data.Structures.Creature;
using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.SkillEngine.Effects
{
    class EfTigersRage : EfDefault
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

            impact.ChangeOfRageIncreaseRate = (int)increase;
            IsUpdateStats = true;
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
