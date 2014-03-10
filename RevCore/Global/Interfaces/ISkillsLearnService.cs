using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global.Interfaces
{
    public interface ISkillsLearnService : IComponent
    {
        void Init();
        void AddStartPlayerAbility(Player Player);
        void LearnAbility(Player Player, int AbilityId, int AbilityLevel);
        void LevelUpAbility(Player player, int AbilityId, int AbilityPoint);
        void LearnSkill(Player player, int skillId);
    }
}
