using Hik.Communication.Scs.Communication.Messages;

namespace AgentServer.InnerNetwork
{
    public class InnerNetworkMessage : ScsMessage
    {
        public short OpCode;

        public byte[] Data;
    }
}
