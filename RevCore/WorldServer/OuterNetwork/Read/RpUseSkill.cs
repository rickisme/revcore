using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpUseSkill : OuterNetworkRecvPacket
    {
        protected int SkillId;

        public override void Read()
        {
            SkillId = ReadD();
        }

        public override void Process()
        {
            UseSkillArgs Args = new UseSkillArgs();
            Args.SkillId = SkillId;

            Global.Global.SkillEngine.UseSkill(Connection, Args);
        }
    }
}
