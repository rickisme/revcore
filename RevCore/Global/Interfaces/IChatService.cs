using Data.Enums;
using Data.Interfaces;

namespace Global.Interfaces
{
    public interface IChatService : IComponent
    {
        void ProcessMessage(IConnection connection, string playerName, string message, ChatType type);
    }
}
