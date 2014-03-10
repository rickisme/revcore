using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nini.Config;

namespace Data.Config
{
    public class ConfigReader
    {
        private static ConfigReader Instance;

        public IConfigSource source;

        public ConfigReader(string cfgFile)
        {
            source = new IniConfigSource(cfgFile);
        }

        public static ConfigReader GetInstance(string cfgFile)
        {
            Instance = new ConfigReader(cfgFile);
            return Instance;
        }
    }
}
