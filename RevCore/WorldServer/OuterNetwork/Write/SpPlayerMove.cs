using Data.Structures.Player;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerMove : OuterNetworkSendPacket
    {
        protected Player Player;

        protected float X1;
        protected float Y1;
        protected float Z1;

        protected float X2;
        protected float Y2;
        protected float Z2;

        protected float Distance;
        protected int Target;

        public SpPlayerMove(Player player, float x1, float y1, float z1, float x2, float y2, float z2, float distance, int tagert)
        {
            Player = player;

            X1 = x1;
            Y1 = y1;
            Z1 = z1;

            X2 = x2;
            Y2 = y2;
            Z2 = z2;

            Distance = distance;
            Target = tagert;
        }
        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, 2);

            WriteF(writer, X1);
            WriteF(writer, Z1);
            WriteF(writer, Y1);
            
            WriteF(writer, X2);
            WriteF(writer, Z2);
            WriteF(writer, Y2);

            WriteD(writer, 1);
            WriteF(writer, Distance);
            WriteD(writer, Target);

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Player.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
