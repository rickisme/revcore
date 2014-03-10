using System.IO;
using Data.Structures.Account;

namespace AgentServer.OuterNetwork.Write
{
    public enum LoginResponse
    {
        success,
        Banned,
        WrongInfo
    }

    public class SpAuth : OuterNetworkSendPacket
    {
        protected Account Account;
        protected LoginResponse Response;

        public SpAuth(Account acc, LoginResponse resp)
        {
            Account = acc;
            Response = resp;
        }

        public override void Write(BinaryWriter writer)
        {
            switch (Response)
            {
                case LoginResponse.success:
                    WriteH(writer, 0);
                    WriteH(writer, 0);
                    WriteH(writer, Account.name.Length);
                    WriteS(writer, Account.name);
                    WriteC(writer, 0);
                    WriteH(writer, Account.name.Length);
                    WriteS(writer, Account.name);
                    break;
                case LoginResponse.WrongInfo:
                    WriteH(writer, 1);
                    WriteH(writer, 3);
                    break;
            }
        }
    }
}
