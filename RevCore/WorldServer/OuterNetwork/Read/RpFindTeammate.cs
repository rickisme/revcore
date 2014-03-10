using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Read
{
    public class RpFindTeammate : OuterNetworkRecvPacket
    {
        protected int level;
        protected int map_id;
        protected int type;

        public override void Read()
        {
            level = ReadC();
            map_id = ReadH();
            type = ReadC();
        }

        public override void Process()
        {
            Global.Global.TeamService.Find(Connection.Player, type, map_id, level);
        }
    }
}
