using Data.Enums;
using Data.Structures.Player;
using System.IO;

namespace WorldServer.OuterNetwork.Write
{
    public class SpChatMessage : OuterNetworkSendPacket
    {
        protected Player Player;
        protected string Message;
        protected ChatType Type;
        protected bool IsSystemMessage;

        public SpChatMessage(Player player, string message, ChatType type, bool sysmsg = false)
        {
            Player = player;
            Message = message;
            Type = type;
            IsSystemMessage = sysmsg;
        }

        /// <summary>
        /// Send System Message Style
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public SpChatMessage(string message, ChatType type)
        {
            IsSystemMessage = true;
            Message = message;
            Type = type;
        }

        public override void Write(System.IO.BinaryWriter writer)
        {
            WriteC(writer, (byte)Type);

            if (IsSystemMessage)
                WriteSN(writer, "System");
            else
                WriteSN(writer, Player.PlayerData.Name);

            WriteB(writer, new byte[7]);
            WriteS(writer, Message);

            writer.Seek(2, SeekOrigin.Begin);
            WriteH(writer, (int)((IsSystemMessage) ? 0 : Player.UID));
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
