using Data.Structures.Template;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpawnEditor
{
    public class SpawnBuilder
    {
        private static string NpcsBinPath = Path.GetFullPath("data/npcs.bin");
        private static List<NpcTemplate> NpcTemplates = new List<NpcTemplate>();

        public static List<SpawnTemplate> GetSpawnTemplates(string SpawnsBinPath)
        {
            var list = new List<SpawnTemplate>();

            using (FileStream stream = File.OpenRead(SpawnsBinPath))
            {
                list = Serializer.DeserializeWithLengthPrefix<List<SpawnTemplate>>(stream, PrefixStyle.Fixed32);
            }

            return list;
        }

        public static void SaveToBin(string filePath, List<SpawnTemplate> list)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var file = File.Create(filePath))
            {
                Serializer.SerializeWithLengthPrefix<List<SpawnTemplate>>(file, list, PrefixStyle.Fixed32);
            }
        }

        private static void GetNpcTemplates()
        {
            using (FileStream stream = File.OpenRead(NpcsBinPath))
            {
                NpcTemplates = Serializer.DeserializeWithLengthPrefix<List<NpcTemplate>>(stream, PrefixStyle.Fixed32);
            }
        }

        public static Dictionary<int, string> GetNames()
        {
            GetNpcTemplates();

            var dic = new Dictionary<int, string>();

            foreach (var template in NpcTemplates)
            {
                dic.Add(template.ID, template.Name);
            }

            return dic;
        }
    }
}
