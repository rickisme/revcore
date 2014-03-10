using Data.Structures.Creature;
using Data.Structures.SkillEngine;
using WorldServer.SkillEngine.Effects;

namespace WorldServer.SkillEngine
{
    class AbilityEffectsProvider
    {
        protected static void Add(Creature creature, Ability ability, EfDefault effect)
        {
            effect.Creature = creature;
            effect.Ability = ability;

            effect.Init();

            ability.Effect = effect;
            creature.Effects.Add(ability);
        }

        public static void ProvideEffects(Creature creature, Ability ability)
        {
            switch (ability.AbilityId)
            {
                #region Blademan Ability
                // http://fate.netgame.com/gameinfo/character/bladesman
                case 10:
                    Add(creature, ability, new EfImprovedGrip());
                    break;
                case 11:
                    Add(creature, ability, new EfPreciseness());
                    break;
                case 12:
                    Add(creature, ability, new EfFlowingBlade());
                    break;
                case 13:
                    Add(creature, ability, new EfLethalBlow());
                    break;
                case 14:
                    Add(creature, ability, new EfBurningWrath());
                    break;
                case 15:
                    Add(creature, ability, new EfReflectionWall());
                    break;
                case 16:
                    Add(creature, ability, new EfArmorCrush());
                    break;
                case 17:
                    Add(creature, ability, new EfPointPiercing());
                    break;
                case 18:
                    Add(creature, ability, new EfHiddenAftermath());
                    break;
                case 19:
                    Add(creature, ability, new EfIronSkin());
                    break;
                #endregion

                #region Swordman Ability
                // http://fate.netgame.com/gameinfo/character/swordman
                case 20:
                    Add(creature, ability, new EfSharpenedSwords());
                    break;
                case 21:
                    Add(creature, ability, new EfSwordDrift());
                    break;
                case 22:
                    Add(creature, ability, new EfFlowingBlade());
                    break;
                case 23:
                    Add(creature, ability, new EfLethalBlow());
                    break;
                case 24:
                    Add(creature, ability, new EfBurningWrath());
                    break;
                case 25:
                    Add(creature, ability, new EfChiArmor());
                    break;
                case 26:
                    Add(creature, ability, new EfLifeDrainer());
                    break;
                case 27:
                    Add(creature, ability, new EfInstantReflex());
                    break;
                case 28:
                    Add(creature, ability, new EfAuroralSword());
                    break;
                case 29:
                    Add(creature, ability, new EfTigersRage());
                    break;
                #endregion

                #region Spearman Ability
                // http://fate.netgame.com/gameinfo/character/spearman
                case 33:
                    Add(creature, ability, new EfLethalBlow());
                    break;
                #endregion

                #region Bowman Ability
                // http://fate.netgame.com/gameinfo/character/bowman
                case 43:
                    Add(creature, ability, new EfLethalBlow());
                    break;
                #endregion

                #region Medic Ability
                // http://fate.netgame.com/gameinfo/character/healer
                #endregion

                #region Ninja Ability
                // http://fate.netgame.com/gameinfo/character/ninja
                #endregion

                #region Busker Ability
                // http://fate.netgame.com/gameinfo/character/busker
                #endregion

                #region Hanbi Ability

                #endregion
            }
        }
    }
}
