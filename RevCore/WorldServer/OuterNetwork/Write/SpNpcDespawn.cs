using Data.Structures.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    public class SpNpcDespawn : OuterNetworkSendPacket
    {
        protected Npc Npc;

        public SpNpcDespawn(Npc npc)
        {
            Npc = npc;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 1);

            WriteH(writer, (int)Npc.UID);
            WriteH(writer, (int)Npc.UID);

            WriteH(writer, Npc.NpcId);

            WriteD(writer, 1);

            WriteD(writer, Npc.LifeStats.Hp);
            WriteD(writer, Npc.MaxHp);

            WriteF(writer, Npc.Position.X);
            WriteF(writer, Npc.Position.Z);
            WriteF(writer, Npc.Position.Y);

            WriteD(writer, 0x40800000);

            WriteF(writer, Npc.SpawnTemplate.Face1);
            WriteF(writer, Npc.SpawnTemplate.Face2);

            WriteF(writer, Npc.Position.X);
            WriteF(writer, Npc.Position.Z);
            WriteF(writer, Npc.Position.Y);

            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0x0C);
            WriteD(writer, 0);
        }
    }
}
