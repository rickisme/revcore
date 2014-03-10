using Global.Logic;

namespace WorldServer.OuterNetwork.Read
{
    public class RpPremiumRevive : OuterNetworkRecvPacket
    {
        protected long ItemId;

        public override void Read()
        {
            ItemId = ReadQ();
        }

        public override void Process()
        {
            PlayerLogic.RessurectByPremiumItem(Connection.Player, ItemId);
        }
    }
}
