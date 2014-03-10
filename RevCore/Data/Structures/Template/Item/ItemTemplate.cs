using Data.Structures.Template.Item.CategorieStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Template.Item
{
    [ProtoBuf.ProtoContract]
    public class ItemTemplate
    {
        [ProtoBuf.ProtoMember(1)]
        public long Id { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Name { get; set; }

        [ProtoBuf.ProtoMember(3)]
        public int Side { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public int Class { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public int Level { get; set; }

        [ProtoBuf.ProtoMember(6)]
        public int JobLevel { get; set; }

        [ProtoBuf.ProtoMember(7)]
        public int Gender { get; set; }

        [ProtoBuf.ProtoMember(8)]
        public int Category { get; set; }

        [ProtoBuf.ProtoMember(9)]
        public int SubCategory { get; set; }

        [ProtoBuf.ProtoMember(10)]
        public int Weight { get; set; }

        [ProtoBuf.ProtoMember(11)]
        public int MinAttack { get; set; }

        [ProtoBuf.ProtoMember(12)]
        public int MaxAttack { get; set; }

        [ProtoBuf.ProtoMember(13)]
        public int Defense { get; set; }

        [ProtoBuf.ProtoMember(14)]
        public int Accuracy { get; set; }

        [ProtoBuf.ProtoMember(15)]
        public int Price { get; set; }

        [ProtoBuf.ProtoMember(16)]
        public string Description { get; set; }

        private static readonly ItemTemplate NullTemplate = new ItemTemplate();

        public static ItemTemplate Factory(long id)
        {
            return !Data.ItemTemplates.ContainsKey(id) ? NullTemplate : Data.ItemTemplates[id];
        }
    }
}
