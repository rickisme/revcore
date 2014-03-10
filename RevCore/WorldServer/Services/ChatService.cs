using Data.Enums;
using Data.Structures.Player;
using Global.Interfaces;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Services
{
    class ChatService : Global.Global, IChatService
    {
        public void Action()
        {
            
        }

        public void ProcessMessage(Data.Interfaces.IConnection connection, string playerName, string message, Data.Enums.ChatType type)
        {
            switch (type)
            {
                case ChatType.Whisper:
                    Player pl = PlayerService.GetPlayerByName(playerName);
                    if (pl != null)
                    {
                        new SpChatMessage(pl, message, type).Send(connection);
                        new SpChatMessage(connection.Player, message, type).Send(pl.Connection);
                    }
                    else
                    {
                        new SpChatMessage(connection.Player, "Player is not online", ChatType.Info).Send(connection);
                    }
                    break;
                default:
                    VisibleService.Send(connection.Player, new SpChatMessage(connection.Player, message, type));
                    break;
            }
        }
    }
}
