using Data.Interfaces;
using System.Collections.Generic;

namespace Data.Structures.SkillEngine
{
    [ProtoBuf.ProtoContract]
    public class Ability
    {
        [ProtoBuf.ProtoMember(1)]
        public int AbilityId { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public double FirstLevel { get; set; }

        [ProtoBuf.ProtoMember(3)]
        public double Step { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public int Time { get; set; }

        public int level;

        public IEffect Effect;
    }
}
