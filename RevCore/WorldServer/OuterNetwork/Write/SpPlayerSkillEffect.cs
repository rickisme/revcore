using Data.Structures.Player;

namespace WorldServer.OuterNetwork.Write
{
    public class SpPlayerSkillEffect : OuterNetworkSendPacket
    {
        protected Player Player;
        protected int UNK;

        public SpPlayerSkillEffect(Player player, int unk)
        {
            Player = player;
            UNK = unk;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteD(writer, Player.UID);
            WriteD(writer, UNK);
        }
    }
}
