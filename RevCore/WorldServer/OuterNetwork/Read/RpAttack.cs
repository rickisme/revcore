using Data.Structures;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using Global.Logic;
using System.Linq;
using Utilities;

namespace WorldServer.OuterNetwork.Read
{
    public class RpAttack : OuterNetworkRecvPacket
    {
        protected int TargetUid;
        protected UseSkillArgs Args = new UseSkillArgs();

        public override void Read()
        {
            TargetUid = ReadD();
            Args.SkillId = ReadD(); // skill id ?
            Args.TargetPosition.X = ReadF();
            Args.TargetPosition.Y = ReadF();
            Args.TargetPosition.Z = ReadF();
        }

        public override void Process()
        {
            Connection.Player.Target = Global.Global.VisibleService.FindTarget(Connection.Player, TargetUid);
            Global.Global.SkillEngine.UseSkill(Connection, Args);
        }
    }
}
