using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Enums;
using System.ComponentModel;
using Data.Structures.Template.Item;

namespace Data.Structures.Creature
{
    [ProtoBuf.ProtoContract]
    public class CreatureBaseStats
    {
        [ProtoBuf.ProtoMember(1)]
        [Category("Commons")]
        public PlayerClass PlayerClass { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public int NpcId = 0;

        [ProtoBuf.ProtoMember(3)]
        public string NpcName = "";

        [ProtoBuf.ProtoMember(4)]
        [Category("Commons")]
        public int Level { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public int NpcHuntingZoneId = 0;

        [ProtoBuf.ProtoMember(6)]
        [Category("Commons")]
        public long Exp { get; set; }

        //HpMpSp

        [ProtoBuf.ProtoMember(10)]
        [Category("LifeStats")]
        public int HpBase { get; set; }
        [ProtoBuf.ProtoMember(11)]
        [Category("LifeStats")]
        public int MpBase { get; set; }
        [ProtoBuf.ProtoMember(12)]
        [Category("LifeStats")]
        public int SpBase { get; set; }

        //Combat

        [ProtoBuf.ProtoMember(20)]
        [Category("Combat")]
        public int Attack { get; set; }
        [ProtoBuf.ProtoMember(21)]
        [Category("Combat")]
        public int Defense { get; set; }
        [ProtoBuf.ProtoMember(22)]
        [Category("Combat")]
        public int Accuracy { get; set; }
        [ProtoBuf.ProtoMember(23)]
        [Category("Combat")]
        public int Dodge { get; set; }

        public int SkillAttack { get; set; }
        public int SkillDefense { get; set; }

        //Stats

        [ProtoBuf.ProtoMember(30)]
        [Category("Stats")]
        public int Spirit { get; set; }
        [ProtoBuf.ProtoMember(31)]
        [Category("Stats")]
        public int Strength { get; set; }
        [ProtoBuf.ProtoMember(32)]
        [Category("Stats")]
        public int Stamina { get; set; }
        [ProtoBuf.ProtoMember(33)]
        [Category("Stats")]
        public int Dexterity { get; set; }

        //Regen

        [ProtoBuf.ProtoMember(50)]
        [Category("LifeStats")]
        public int NaturalMpRegen { get; set; }

        // Additional stats
        public int CriticalAttackRate = 0;
        public int SkillCriticalRate = 0;
        public int ComboAttackRate = 0;
        public int RageModeDuration = 15;
        public int ReflectChance = 0;
        public int BlockDamageChance = 0;
        public int ArmorBreakRate = 0;
        public int AddAttackPowerRate = 0;
        public int DrainerRate = 0;
        public int SkillDodgeRate = 0;
        public int RageAttackDamagerRate = 0;
        public int RageIncreaseRate = 0;

        public List<Passivity> Passivities = new List<Passivity>();

        public override string ToString()
        {
            if (NpcHuntingZoneId == 0)
                return string.Format("Player: {0}", PlayerClass);

            return string.Format("Npc: {0} [{1}:{2}]", NpcName, NpcHuntingZoneId, NpcId);
        }

        public CreatureBaseStats Clone()
        {
            return (CreatureBaseStats)MemberwiseClone();
        }

        public void CopyTo(CreatureBaseStats gameStats)
        {
            //HpMp
            gameStats.HpBase = HpBase;
            gameStats.MpBase = MpBase;
            gameStats.SpBase = SpBase;

            //Combat
            gameStats.Attack = Attack;
            gameStats.Defense = Defense;
            gameStats.Accuracy = Accuracy;
            gameStats.Dodge = Dodge;

            //Stats
            gameStats.Spirit = Spirit;
            gameStats.Strength = Strength;
            gameStats.Stamina = Stamina;
            gameStats.Dexterity = Dexterity;

            //Regen
            gameStats.NaturalMpRegen = NaturalMpRegen;

            gameStats.Passivities = new List<Passivity>();
        }
    }
}
