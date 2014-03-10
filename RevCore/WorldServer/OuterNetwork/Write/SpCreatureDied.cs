using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Player;
using System.IO;
using Utilities;

namespace WorldServer.OuterNetwork.Write
{
    public class SpCreatureDied : OuterNetworkSendPacket
    {
        protected Creature Creature;

        public SpCreatureDied(Creature creature)
        {
            Creature = creature;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 0);

            writer.Seek(2, SeekOrigin.Begin);
            if (Creature is Player)
                WriteH(writer, (int)(Creature as Player).UID);
            else if (Creature is Npc)
                WriteH(writer, (int)(Creature as Npc).UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
