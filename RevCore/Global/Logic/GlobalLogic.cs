using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global.Logic
{
    public class GlobalLogic : Global
    {
        public static void ServerStart()
        {
            Data.Data.LoadAll();

            StatsService.Init();
            MapService.Init();

            InitMainLoop();
        }

        public static void AttackStageEnd(Creature creature)
        {
            //FeedbackService.AttackStageEnd(creature);
            Player player = creature as Player;
            if (player != null)
                Global.SkillEngine.UseSkill(player.Connection, creature.Attack.Args);

            Npc npc = creature as Npc;
            if (npc != null)
                Global.SkillEngine.UseSkill(npc, null);
        }

        public static void AttackFinished(Creature creature)
        {
            FeedbackService.AttackFinished(creature);
            SkillEngine.AttackFinished(creature);
        }
    }
}
