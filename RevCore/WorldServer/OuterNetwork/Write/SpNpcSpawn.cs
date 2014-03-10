using Data.Structures.Npc;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpNpcSpawn : OuterNetworkSendPacket
    {
        protected Npc Npc;

        public SpNpcSpawn(Npc npc)
        {
            Npc = npc;
        }

        public override void Write(BinaryWriter writer)
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
