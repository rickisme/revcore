using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using DatabaseFactory;
using Global.Interfaces;
using System;
using System.Collections.Generic;
using Utilities;

namespace WorldServer.Services
{
    class SkillsLearnService : ISkillsLearnService
    {
        public void Init()
        {
            
        }

        public void AddStartPlayerAbility(Player Player)
        {
            // todo correct retail ability list

            switch (Player.PlayerData.Class)
            {
                case PlayerClass.Blademan:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(10, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(11, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(12, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(13, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(14, 0));
                    break;
                case PlayerClass.Swordman:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(20, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(21, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(22, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(23, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(24, 0));
                    break;
                case PlayerClass.Spearman:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(30, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(31, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(32, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(33, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(34, 0));
                    break;
                case PlayerClass.Bowman:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(40, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(41, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(42, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(43, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(44, 0));
                    break;
                case PlayerClass.Medic:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(50, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(51, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(52, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(53, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(54, 0));
                    break;
                case PlayerClass.Ninja:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(60, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(61, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(62, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(63, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(64, 0));
                    break;
                case PlayerClass.Busker:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(70, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(71, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(72, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(73, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(74, 0));
                    break;
                case PlayerClass.Hanbi:
                    Player.Abilities.Add(0, new KeyValuePair<int, int>(10, 0));
                    Player.Abilities.Add(1, new KeyValuePair<int, int>(11, 0));
                    Player.Abilities.Add(2, new KeyValuePair<int, int>(12, 0));
                    Player.Abilities.Add(3, new KeyValuePair<int, int>(13, 0));
                    Player.Abilities.Add(4, new KeyValuePair<int, int>(14, 0));
                    break;
            }

            DataBaseAbility.SavePlayerAbility(Player, SkillType.Basic, false);
        }

        public void LearnAbility(Player Player, int AbilityId, int AbilityLevel)
        {
            Player.Abilities.AddAbility(AbilityId, AbilityLevel);
        }

        public void LevelUpAbility(Player Player, int AbilityId, int AbilityPoint)
        {
            if (AbilityPoint <= 0)
                return;

            if (AbilityPoint != Player.AbilityPoint) // check hack ability point
            {
                Log.Debug("AbilityPoint({0}) : Player.AbilityPoint({1})", AbilityPoint, Player.AbilityPoint);
                Log.Debug("Hack LevelUpAbility: PlayerId={0} - {1}", Player.PlayerId, DateTime.Now);
                return;
            }

            if (!Player.Abilities.LevelUpAbility(AbilityId))
                return;

            Player.AbilityPoint -= 1;

            //SkillEngine.SkillEngine.AbnormalityProcessor.ApplyAbility(Player);
            //SkillEngine.SkillEngine.AbilityProcessor.ApplyAbility(Player);
            SkillEngine.SkillEngine.AbilityProcessor.UpdateAbility(Player, AbilityId);
            Global.Global.StatsService.UpdateStats(Player);
        }

        public void LearnSkill(Player player, int skillId)
        {
            Skill skill = Data.Data.Skills[skillId];

            if (skill == null)
            {
                Log.Warn("LearnSkill: skill data not found");
                return;
            }

            if (player.GetLevel() < skill.Level)
            {
                Log.Warn("LearnSkill: player level < skill Level");
                return;
            }

            if (player.GetJobLevel() < skill.JobLevel)
            {
                Log.Warn("LearnSkill: player Job level < skill Job Level");
                return;
            }

            if (skill.Job != (int)player.PlayerData.Class)
            {
                Log.Warn("LearnSkill: player Job < skill Job");
                return;
            }

            if (player.SkillPoint < skill.LearnExp && (player.PlayerData.Class != PlayerClass.Hanbi && skill.Id != 1000101))
            {
                Log.Warn("LearnSkill: player SkillPoint < skill LearnExp");
                return;
            }

            if ((player.PlayerData.Class != PlayerClass.Hanbi && skill.Id != 1000101))
                player.SkillPoint -= skill.LearnExp;

            switch ((SkillType)skill.Type)
            {
                case SkillType.Basic:
                    player.Skills.AddSkill(skill.Id, 1);
                    break;
                case SkillType.Passive:
                    player.PassiveSkills.AddSkill(skill.Id, 1);
                    break;
                case SkillType.Ascension:
                    player.AscensionSkills.AddSkill(skill.Id, 1);
                    break;
            }

            DataBaseSkill.AddPlayerSkill(player, skill.Id, 1, (SkillType)skill.Type, skill.Index);
        }

        public void Action()
        {
            
        }
    }
}
