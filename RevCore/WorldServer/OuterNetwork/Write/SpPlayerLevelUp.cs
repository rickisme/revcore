using Data.Structures.Player;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerLevelUp : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerLevelUp(Player player)
        {
            Player = player;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteC(writer, Player.Level);
            WriteC(writer, Player.Level - 1);


            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)Player.UID);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
