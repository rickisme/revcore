using Data.Config;
using Data.Enums;
using Nini.Config;
using System.IO;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork
{
    public class SystemMessages
    {
        private static string SystemCfgPath = Path.GetFullPath("Config/SysMessage.ini");

        private static IConfig Configs = ConfigReader.GetInstance(SystemCfgPath).source.Configs["SYSTEM_MESSAGE"];

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static SpChatMessage EnterGameMessage = new SpChatMessage(Configs.GetString("ENTER_GAME"), ChatType.Announce);

        public static SpChatMessage InventoryIsFull = new SpChatMessage(Configs.GetString("INVENTORY_ISFULL"), ChatType.UNK1);

        public static SpChatMessage YouCantPickUpItem = new SpChatMessage(Configs.GetString("CANT_PICK_ITEM"), ChatType.UNK1);
    }
}
