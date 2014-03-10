using Data.Structures.Creature;
using Data.Structures.Player;
using Global.Logic;

namespace WorldServer.SkillEngine.Effects
{
    class EfInstantReflex : EfDefault
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
            increase2 = 0.6;

            for (int i = 1; i < Ability.level; i++)
            {
                increase += Ability.Step;
                increase2 += 0.6;
            }

            double percent = 1 + (increase2 / 100);

            Player.GameStats.SkillAttack = (int)(baseStats.SkillAttack * percent);
            impact.ChangeOfSkillDodgeRate = (int)increase;
            IsUpdateStats = true;
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
