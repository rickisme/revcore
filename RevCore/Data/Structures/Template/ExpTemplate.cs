using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Template
{
    [ProtoBuf.ProtoContract]
    public class ExpTemplate
    {
        [ProtoBuf.ProtoMember(1)]
        public int Level;

        [ProtoBuf.ProtoMember(2)]
        public long Experience;
    }
}
