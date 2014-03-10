using Data.Structures;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Global.Logic;
using System.Linq;
using Utilities;

namespace WorldServer.OuterNetwork.Read
{
    public class RpTargetSelected : OuterNetworkRecvPacket
    {
        protected int TargetUid;

        public override void Read()
        {
            TargetUid = ReadD();
        }

        public override void Process()
        {
            Log.Debug("TargetUid = {0}", TargetUid);

            Creature target = Connection
                .Player
                .Instance
                .Npcs
                .Where(v => v.UID == TargetUid)
                .FirstOrDefault() as Creature;

            Connection.Player.Target = target;

            /*if (target != null)
                PlayerLogic.MarkTarget(Connection, target);*/
        }
    }
}
