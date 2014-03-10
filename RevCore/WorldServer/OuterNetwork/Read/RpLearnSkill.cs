using Global.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpLearnSkill : OuterNetworkRecvPacket
    {
        protected int SkillId;

        public override void Read()
        {
            SkillId = ReadD();
        }

        public override void Process()
        {
            //Global.Global.SkillsLearnService.LearnSkill(Connection.Player, SkillId);
            PlayerLogic.PlayerLearnSkill(Connection.Player, SkillId);
        }
    }
}
