using Hik.Communication.Scs.Communication.Messages;

namespace AgentServer.OuterNetwork
{
    public class OuterNetworkMessage : ScsMessage
    {
        public short OpCode;

        public byte[] Data;
    }
}
