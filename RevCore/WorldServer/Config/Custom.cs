using Data.Config;
using Nini.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldServer.Config
{
    public class Custom
    {
        private static string RateCfgPath = System.IO.Path.GetFullPath("Config/Custom.ini");

        private static IConfig Config = ConfigReader.GetInstance(RateCfgPath).source.Configs["CUSTOM_CONFIG"];

        public static int MONEY_DROP_STYLE = Config.GetInt("MONEY_DROP_STYLE");
    }
}
