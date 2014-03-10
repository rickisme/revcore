using Data.Interfaces;

namespace Global.Interfaces
{
    public interface IAccountService : IComponent
    {
        void Authorized(IConnection connection, string accountName, string password);
        void AbortExitAction(IConnection connection);
    }
}
