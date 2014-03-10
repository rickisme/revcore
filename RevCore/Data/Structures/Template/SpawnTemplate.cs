using System.ComponentModel;

namespace Data.Structures.Template
{
    [ProtoBuf.ProtoContract]
    public class SpawnTemplate
    {
        [ProtoBuf.ProtoMember(1)]
        [Category("Info")]
        public int NpcId { get; set; }

        [ProtoBuf.ProtoMember(2)]
        [Category("Info")]
        public short Type { get; set; }

        [ProtoBuf.ProtoMember(3)]
        [Category("Info")]
        public bool GroupSpawn { get; set; }

        [ProtoBuf.ProtoMember(4)]
        [Category("Info")]
        public int Count { get; set; }

        

        [ProtoBuf.ProtoMember(5)]
        [Category("Position")]
        public int MapId { get; set; }

        [ProtoBuf.ProtoMember(6)]
        [Category("Position")]
        public float X { get; set; }

        [ProtoBuf.ProtoMember(7)]
        [Category("Position")]
        public float Y { get; set; }

        [ProtoBuf.ProtoMember(8)]
        [Category("Position")]
        public float Z { get; set; }

        [ProtoBuf.ProtoMember(9)]
        [Category("Position")]
        public float Face1 { get; set; }

        [ProtoBuf.ProtoMember(10)]
        [Category("Position")]
        public float Face2 { get; set; }

        public SpawnTemplate Clone()
        {
            return (SpawnTemplate)MemberwiseClone();
        }
    }
}
