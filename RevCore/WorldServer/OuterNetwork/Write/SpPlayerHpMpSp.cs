using Data.Structures.Player;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerHpMpSp : OuterNetworkSendPacket
    {
        protected Player Player;

        public SpPlayerHpMpSp(Player p)
        {
            Player = p;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);

            WriteD(writer, Player.LifeStats.Hp);
            WriteD(writer, Player.LifeStats.Mp);
            WriteD(writer, Player.LifeStats.Sp);

            WriteD(writer, Player.MaxHp);
            WriteD(writer, Player.MaxMp);
            WriteD(writer, Player.MaxSp);

            WriteD(writer, 0);
        }
    }
}
