using Data.Structures.Creature;
using Data.Structures.Player;
using Global.Logic;

namespace WorldServer.SkillEngine.Effects
{
    class EfIronSkin : EfDefault
    {
        public override void Init()
        {
            CreatureLogic.UpdateCreatureStats(Creature);
        }

        public override void SetImpact(CreatureEffectsImpact impact)
        {
            if (IsUpdateStats)
                return;

            Player Player = Creature as Player;
            CreatureBaseStats baseStats = Global.Global.StatsService.GetBaseStats(Player);

            double increase = 0.0;
            double increase2 = 0.0;

            if (Ability.level < 1)
                return;

            increase = Ability.FirstLevel;
            increase2 = 2.0;

            for (int i = 1; i < Ability.level; i++)
            {
                increase += Ability.Step;
                increase2 += 2.0;
            }

            double percent = 1 + (increase / 100);

            impact.ChangeOfAttack = (int)(baseStats.Attack * percent);
            impact.ChangeOfHp = (int)increase2;
            IsUpdateStats = true;
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
