using Data.Interfaces;
using Global.Interfaces;
using AgentServer.OuterNetwork;
using Utilities;
using AgentServer.OuterNetwork.Write;
using DatabaseFactory;

namespace AgentServer.Services
{
    public class AccountService : IAccountService
    {
        public void Action()
        {
            
        }

        public void Authorized(IConnection connection, string accountName, string password)
        {
            ((OuterNetworkConnection)connection).Account = DataBaseAccount.GetAccount(accountName, password);
            var account = ((OuterNetworkConnection)connection).Account;
            new SpAuth(account, (account.name != null) ? LoginResponse.success : LoginResponse.WrongInfo).Send(connection);
        }


        public void AbortExitAction(IConnection connection)
        {
            
        }
    }
}
