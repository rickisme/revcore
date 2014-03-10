using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Template
{
    [ProtoBuf.ProtoContract]
    public class NpcTemplate
    {
        [ProtoBuf.ProtoMember(1)]
        public int ID { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Name { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public int HealthPoint { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public int Attack { get; set; }

        [ProtoBuf.ProtoMember(6)]
        public int Defense { get; set; }

        [ProtoBuf.ProtoMember(7)]
        public int Npc { get; set; }

        [ProtoBuf.ProtoMember(8)]
        public int Level { get; set; }

        [ProtoBuf.ProtoMember(9)]
        public int Exp { get; set; }

        [ProtoBuf.ProtoMember(10)]
        public int Auto { get; set; }

        [ProtoBuf.ProtoMember(11)]
        public int Boss { get; set; }

        private static readonly NpcTemplate NullTemplate = new NpcTemplate();

        public static NpcTemplate Factory(int id)
        {
            return !Data.NpcTemplates.ContainsKey(id) ? NullTemplate : Data.NpcTemplates[id];
        }
    }
}
