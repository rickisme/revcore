using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.Services;

namespace WorldServer.OuterNetwork.Read
{
    public class RpOpenDialog : OuterNetworkRecvPacket
    {
        protected int dialogId;
        protected int type;
        public override void Read()
        {
            dialogId = ReadH(); // 
            ReadH();
            ReadD();
            type = ReadH();
        }
        public override void Process()
        { 
            Player player= Connection.Player;
            int npcId = (int)player.Target.UID;
            DialogService.OpenDialog(player, dialogId, type, npcId);
        }
    }
}
