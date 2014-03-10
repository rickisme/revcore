using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data.Structures.Creature;
using ProtoBuf;

namespace StatEditor
{
    public class StatBuilder
    {
        public static string BinPath = Path.GetFullPath("data/stats.bin");

        public static List<CreatureBaseStats> Stats = new List<CreatureBaseStats>();

        public static void GetStatTemplate()
        {
            if (!File.Exists("Data/stats.bin"))
            {
                using (var file = File.Create("Data/stats.bin"))
                {
                    Serializer.SerializeWithLengthPrefix<List<CreatureBaseStats>>(file, Stats, PrefixStyle.Fixed32);
                }
            }

            using (FileStream stream = File.OpenRead("data/stats.bin"))
            {
                Stats = Serializer.DeserializeWithLengthPrefix<List<CreatureBaseStats>>(stream, PrefixStyle.Fixed32).ToList();
            }
        }

        public static void Save()
        {
            if (File.Exists(BinPath))
                File.Delete(BinPath);

            using (var file = File.Create("Data/stats.bin"))
            {
                Serializer.SerializeWithLengthPrefix<List<CreatureBaseStats>>(file, Stats, PrefixStyle.Fixed32);
            }
        }
    }
}
