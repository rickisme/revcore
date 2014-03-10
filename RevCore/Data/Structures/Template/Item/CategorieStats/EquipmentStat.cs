namespace Data.Structures.Template.Item.CategorieStats
{
    [ProtoBuf.ProtoContract]
    public class EquipmentStat : IDefaultCategorieStat
    {
        [ProtoBuf.ProtoMember(1)]
        public int EquipmentId;

        [ProtoBuf.ProtoMember(2)]
        public int Def;

        [ProtoBuf.ProtoMember(3)]
        public int Accuracy;

        [ProtoBuf.ProtoMember(4)]
        public int MinAtk;

        [ProtoBuf.ProtoMember(5)]
        public int MaxAtk;
    }
}
