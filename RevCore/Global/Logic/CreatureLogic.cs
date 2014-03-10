using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;

namespace Global.Logic
{
    public class CreatureLogic : Global
    {
        public static CreatureBaseStats InitGameStats(Creature creature)
        {
            return StatsService.InitStats(creature);
        }

        public static void UpdateCreatureStats(Creature creature)
        {
            StatsService.UpdateStats(creature);

            Player player = creature as Player;
            if (player != null)
                FeedbackService.StatsUpdated(player);
        }

        public static void HpChanged(Creature creature, int diff, Creature attacker = null)
        {
            if (creature is Player)
            {
                FeedbackService.HpMpSpChanged(creature as Player);
                //PartyService.SendLifestatsToPartyMembers(((Player)creature).Party);
            }

            ObserverService.NotifyHpMpSpChanged(creature);
        }

        public static void MpChanged(Creature creature, int diff, Creature attacker = null)
        {
            ObserverService.NotifyHpMpSpChanged(creature);

            if (creature is Player)
            {
                FeedbackService.HpMpSpChanged(creature as Player);
                //PartyService.SendLifestatsToPartyMembers(((Player)creature).Party);
            }
        }

        public static void NpcDied(Npc npc)
        {
            var player = npc.Ai.GetKiller() as Player;

            if (player != null)
            {
                /*if (player.Party != null)
                    foreach (Player member in PartyService.GetOnlineMembers(player.Party))
                        QuestEngine.OnPlayerKillNpc(member, npc);
                else
                    QuestEngine.OnPlayerKillNpc(player, npc);*/

                player.Instance.OnNpcKill(player, npc);
                player.Target = null;
            }

            npc.Ai.DealExp();

            if (player != null)
            {
                MapService.CreateDrop(npc, player);
            }
        }
    }
}
