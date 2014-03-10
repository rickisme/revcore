using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Global.Interfaces;
using Global.Logic;
using System;
using System.Collections.Generic;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.SkillEngine
{
    internal class SkillEngine : Global.Global, ISkillEngine
    {
        public static AbilityProcessor AbilityProcessor = new AbilityProcessor();

        public void Init()
        {
            
        }

        public void UseSkill(IConnection connection, UseSkillArgs args)
        {
            Player player = connection.Player;
            Skill skill = args.GetSkill(player);

            ProcessSkill(player, args, skill);
        }

        public void UseSkill(IConnection connection, List<UseSkillArgs> argsList)
        {
            
        }

        public void UseSkill(Npc npc, Skill skill)
        {
            if (npc.Target == null)
                return;

            ProcessSkill(npc, new UseSkillArgs
            {
                IsTargetAttack = false,
                SkillId = (skill != null) ? skill.Id : 0,
                TargetPosition = npc.Target.Position.Clone(),
            }, skill);
        }

        public void MarkTarget(IConnection connection, Creature target)
        {
            connection.Player.Target = target;
        }

        public void ReleaseAttack(Player player, int attackUid, int type)
        {
            
        }

        public void AttackFinished(Creature creature)
        {
            creature.Target = null;
        }

        private void ProcessSkill(Creature creature, UseSkillArgs args, Skill skill)
        {
            Creature target = creature.Target;

            if (args.SkillId != 0 && skill == null)
            {
                Player p = creature as Player;

                if (args.SkillId == 1)
                    p.PlayerMode = PlayerMode.Relax;

                if (p != null)
                    VisibleService.Send(p, new SpPlayerSetSpell(args.SkillId, 1, 1));
            }
            else if (skill != null)
            {
                ProcessSkill(creature, args, skill, 0);
            }
            else
            {
                int time = (creature is Player) ? 1200 : 2000;
                ProcessAttack(creature, args, time);
            }
        }

        private void ProcessSkill(Creature creature, UseSkillArgs args, Skill skill, int time)
        {
            try
            {
                Player player = creature as Player;
                Creature target = creature.Target;

                if (target == null || creature.LifeStats.IsDead())
                    return;

                if (creature.LifeStats.Mp < skill.ManaCost)
                    if (player != null)
                        new SpPlayerSetSpell(args.SkillId, 2, 0).Send(player);

                if (!target.LifeStats.IsDead())
                {
                    creature.Attack = new Attack(creature,
                                             args,
                                             () => GlobalLogic.AttackStageEnd(creature),
                                             () => GlobalLogic.AttackFinished(creature));

                    int damage = SeUtils.CalculateDefaultAttackDamage(creature, target, creature.GameStats.Attack);

                    if (player != null)
                        VisibleService.Send(player, new SpAttack(player, player.Attack));

                    Npc npc = creature as Npc;
                    if (npc != null)
                        VisibleService.Send(npc, new SpNpcAttack(npc, npc.Attack));

                    switch (skill.Type)
                    {
                        case 2:

                            break;
                        case 3:

                            break;
                        default:
                            creature.LifeStats.MinusMp(skill.ManaCost);
                            break;
                    }

                    target.LifeStats.MinusHp(damage);

                    AiLogic.OnAttack(creature, target);
                    AiLogic.OnAttacked(target, creature, damage);

                    if (target is Player)
                        (target as Player).LifeStats.PlusSp(damage);

                    new DelayedAction(creature
                        .Attack
                        .NextStage, time);

                    return;
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("ProcessSkill:", ex);
            }
        }

        private void ProcessAttack(Creature creature, UseSkillArgs args, int time)
        {
            try
            {
                Creature target = creature.Target;

                if (target == null || creature.LifeStats.IsDead())
                    return;

                if (!target.LifeStats.IsDead())
                {
                    creature.Attack = new Attack(creature,
                                             args,
                                             () => GlobalLogic.AttackStageEnd(creature),
                                             () => GlobalLogic.AttackFinished(creature));

                    int damage = SeUtils.CalculateDefaultAttackDamage(creature, target, creature.GameStats.Attack);

                    Player player = creature as Player;
                    if (player != null)
                        VisibleService.Send(player, new SpAttack(player, player.Attack));

                    Npc npc = creature as Npc;
                    if (npc != null)
                        VisibleService.Send(npc, new SpNpcAttack(npc, npc.Attack));

                    target.LifeStats.MinusHp(damage);

                    AiLogic.OnAttack(creature, target);
                    AiLogic.OnAttacked(target, creature, damage);

                    if (target is Player)
                        (target as Player).LifeStats.PlusSp(damage);

                    new DelayedAction(creature
                        .Attack
                        .NextStage, time);

                    return;
                }

                new DelayedAction(creature
                    .Attack
                    .Finish, time);
            }
            catch (Exception ex)
            {
                Log.ErrorException("ProcessAttack:", ex);
            }
        }

        public void Action()
        {
            AbilityProcessor.Action();
        }
    }
}
