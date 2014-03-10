using Data.Structures.Player;

namespace WorldServer.OuterNetwork.Write
{
    public class SpLearnSkillMessage : OuterNetworkSendPacket
    {
        protected Player player;

        public SpLearnSkillMessage(Player player) 
        {
            this.player = player;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteQ(writer, 1);
            WriteB(writer, new byte[304]);
            WriteD(writer, player.SkillPoint);
        }
    }
}
