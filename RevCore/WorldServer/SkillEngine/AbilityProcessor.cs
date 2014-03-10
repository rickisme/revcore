using Data.Structures.Creature;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using System.Collections.Generic;
using System.Threading;

namespace WorldServer.SkillEngine
{
    class AbilityProcessor
    {
        public List<Creature> targets = new List<Creature>();

        public object TargetsLock = new object();

        public void ApplyAbility(Player player)
        {
            foreach (var abi in player.Abilities.Values)
            {
                AddAbility(player, abi.Key, abi.Value);
            }
        }

        public void AddAbility(Creature target, int id, int level)
        {
            if (target == null)
                return;

            lock (target.EffectsLock)
            {
                for (int i = 0; i < target.Effects.Count; i++)
                {
                    if (target.Effects[i].AbilityId == id)
                        target.Effects.Remove(target.Effects[i]);
                }
            }

            Ability abi = Data.Data.Abilities[id];

            if (abi == null)
                return;

            abi.level = level;

            AbilityEffectsProvider.ProvideEffects(target, abi);

            lock (target.EffectsLock)
            {
                target.Effects.Add(abi);
            }

            lock (TargetsLock)
                if (!targets.Contains(target))
                    targets.Add(target);
        }

        public void UpdateAbility(Creature target, int id)
        {
            if (target == null)
                return;

            if (targets.Contains(target))
                targets.Remove(target);

            lock (target.EffectsLock)
            {
                for (int i = 0; i < target.Effects.Count; i++)
                {
                    if (target.Effects[i].AbilityId == id)
                    {
                        target.Effects[i].level += 1;
                        target.Effects[i].Effect.SetActive(false);
                        break;
                    }
                }
            }

            targets.Add(target);
        }

        public void Action()
        {
            lock (TargetsLock)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    CheckCreatureAbility(targets[i]);

                    if ((i & 511) == 0) // 2^N - 1
                        Thread.Sleep(1);
                }
            }
        }

        private void CheckCreatureAbility(Creature creature)
        {
            if (creature == null)
                return;

            if (creature.Effects == null)
                return;

            for (int i = 0; i < creature.Effects.Count; i++)
                creature.Effects[i].Effect.Action();
        }
    }
}
