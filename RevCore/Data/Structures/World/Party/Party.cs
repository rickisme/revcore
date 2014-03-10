using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.World.Party
{
    public class Party
    {
        public List<Player.Player> PartyMembers;
        public object MemberLock = new object();
        public long Exp;
    }
}
