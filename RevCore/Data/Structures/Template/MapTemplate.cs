using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Template
{
    [ProtoBuf.ProtoContract]
    public class MapTemplate
    {
        [ProtoBuf.ProtoMember(1)]
        public int ID;

        [ProtoBuf.ProtoMember(2)]
        public string Name;

        [ProtoBuf.ProtoMember(3)]
        public string File;

        [ProtoBuf.ProtoMember(4)]
        public int X;

        [ProtoBuf.ProtoMember(5)]
        public int Y;
    }
}
