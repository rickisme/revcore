using Global.Logic;

namespace WorldServer.OuterNetwork.Read
{
    public class RpMove : OuterNetworkRecvPacket
    {
        public float X1;
        public float Y1;
        public float Z1;

        protected float X2;
        protected float Y2;
        protected float Z2;

        protected int unk;
        protected float Distance;
        protected int Target;

        public override void Read()
        {
            ReadD();

            X1 = ReadF();
            Z1 = ReadF();
            Y1 = ReadF();

            X2 = ReadF();
            Z2 = ReadF();
            Y2 = ReadF();

            unk = ReadD();
            Distance = ReadF();
            Target = ReadD();
        }

        public override void Process()
        {
            PlayerLogic.PlayerMoved(Connection.Player, X1, Y1, Z1, X2, Y2, Z2, Distance, Target);
        }
    }
}
