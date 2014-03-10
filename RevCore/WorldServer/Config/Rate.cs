using Data.Config;
using Nini.Config;

namespace WorldServer.Config
{
    public class Rate
    {
        private static string RateCfgPath = System.IO.Path.GetFullPath("Config/Rate.ini");

        private static IConfig Config = ConfigReader.GetInstance(RateCfgPath).source.Configs["GAME_RATE"];

        public static int LEVEL_CAP = Config.GetInt("LEVEL_CAP");
        public static int EXP = Config.GetInt("EXP");
        public static int KI_EXP = Config.GetInt("KI_EXP");
        public static int MONEY = Config.GetInt("MONEY");
    }
}
