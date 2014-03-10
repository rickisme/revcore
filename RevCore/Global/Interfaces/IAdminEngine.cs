using Data.Interfaces;
using Data.Structures.Player;

namespace Global.Interfaces
{
    public interface IAdminEngine : IComponent
    {
        bool ProcessChatMessage(IConnection connection, string message);
        bool IsGM(Player player);
        bool IsDev(Player player);
    }
}
