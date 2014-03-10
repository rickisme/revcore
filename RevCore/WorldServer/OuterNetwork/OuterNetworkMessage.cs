using Hik.Communication.Scs.Communication.Messages;
using Utilities;

namespace WorldServer.OuterNetwork
{
    public class OuterNetworkMessage : ScsMessage
    {
        public short TotalLength;

        public short UserSession;

        public short Opcode;

        public byte[] Data;

        public override string ToString()
        {
            return string.Format("\nTotalLength:{0}\nUserSession:{1}\nOpcode:{2}\nData:\n{3}", TotalLength, UserSession, Opcode, Data.FormatHex());
        }
    }
}
