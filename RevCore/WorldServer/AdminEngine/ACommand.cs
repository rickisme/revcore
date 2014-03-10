using Data.Enums;
using Data.Interfaces;
using System.Threading;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine
{
    public abstract class ACommand
    {
        public virtual void Process(IConnection connection, string[] msg)
        {
            ThreadPool.QueueUserWorkItem(o => ProcessAsync(connection, msg));
        }

        public virtual void ProcessAsync(IConnection connection, string[] msg)
        {

        }

        public void Alert(IConnection connection, string chatMessage)
        {
            new SpChatMessage(connection.Player, chatMessage, ChatType.Info).Send(connection);
        }

        public void Print(IConnection connection, string chatMessage)
        {
            new SpChatMessage(chatMessage, ChatType.Info).Send(connection);
        }

        /// <summary>
        /// Get value from chat how at console.
        /// Don't use in Process override, only in ProcessAsync
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="chatMessage"></param>
        /// <returns>String value</returns>
        public string GetValue(IConnection connection, string chatMessage = null)
        {
            if (chatMessage != null) Alert(connection, chatMessage);

            return new WaitMessageHandle().GetValue(connection);
        }
    }
}
