namespace Data.Structures.SkillEngine
{
    [ProtoBuf.ProtoContract]
    public class Skill
    {
        [ProtoBuf.ProtoMember(1)]
        public int Id { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public int Force { get; set; } //good and evil.;

        [ProtoBuf.ProtoMember(3)]
        public int Job { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public int JobLevel { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public int Level { get; set; }

        [ProtoBuf.ProtoMember(6)]
        public int MaxLevel { get; set; }

        [ProtoBuf.ProtoMember(7)]
        public int ManaCost { get; set; }

        [ProtoBuf.ProtoMember(8)]
        public int LearnExp { get; set; }

        [ProtoBuf.ProtoMember(9)]
        public int Attack { get; set; }

        [ProtoBuf.ProtoMember(10)]
        public int Type { get; set; }

        [ProtoBuf.ProtoMember(11)]
        public int Effect { get; set; }

        [ProtoBuf.ProtoMember(12)]
        public int Index { get; set; }

        [ProtoBuf.ProtoMember(13)]
        public int AttackCount { get; set; }

        [ProtoBuf.ProtoMember(14)]
        public int Time { get; set; }

        private static readonly Skill NullTemplate = new Skill();

        public static Skill Factory(int id)
        {
            return !Data.Skills.ContainsKey(id) ? NullTemplate : Data.Skills[id];
        }
    }
}
