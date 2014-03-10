using Data.Structures.Template;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcEditor
{
    public class NpcBuilder
    {
        public static string BinPath = Path.GetFullPath("data/npcs.bin");

        public static List<NpcTemplate> NpcInfoTemplates = new List<NpcTemplate>();

        public static void GetNpcTemplates()
        {
            using (FileStream stream = File.OpenRead(BinPath))
            {
                NpcInfoTemplates = Serializer.DeserializeWithLengthPrefix<List<NpcTemplate>>(stream, PrefixStyle.Fixed32);
            }
        }
    }
}
