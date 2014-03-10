using Hik.Communication.Scs.Communication.Messages;

namespace WorldServer.InnerNetwork
{
    public class InnerNetworkMessage : ScsMessage
    {
        public short OpCode;

        public byte[] Data;
    }
}
