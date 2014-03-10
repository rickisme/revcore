using Data.Structures.Npc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldServer.OuterNetwork.Write
{
    /// <summary>
    /// this class look like revive npc
    /// </summary>
    public class SpUNK007B : OuterNetworkSendPacket
    {
        protected Npc Npc;

        public SpUNK007B(Npc npc)
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

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Npc.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
