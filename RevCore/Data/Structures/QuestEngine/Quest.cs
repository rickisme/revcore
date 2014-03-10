using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.QuestEngine
{
    [ProtoBuf.ProtoContract]
    public class Quest
    {
        [ProtoBuf.ProtoMember(1)]
        public int Id;
    }
}
