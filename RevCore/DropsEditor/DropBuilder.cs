using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DropsEditor
{
    public class DropBuilder
    {
        public static string BinPath = Path.GetFullPath("data/drops.bin");

        public static Dictionary<int, List<long>> Drop = new Dictionary<int, List<long>>();

        public static byte[] YBBuffer;

        public static void GetDropTemplate()
        {
            #region makefile if not exists
            if (!File.Exists(BinPath))
            {
                using (var file = File.Create(BinPath))
                {
                    Serializer.SerializeWithLengthPrefix<Dictionary<int, List<long>>>(file, Drop, PrefixStyle.Fixed32);
                }
            }
            #endregion

            using (FileStream stream = File.OpenRead(BinPath))
            {
                Drop = Serializer.DeserializeWithLengthPrefix<Dictionary<int, List<long>>>(stream, PrefixStyle.Fixed32);
            }
        }

        public static void ReadYBData()
        {
            using (FileStream Stream = new FileStream("data/ybdrop.dat", FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(Stream))
                {
                    writer.Write(YBBuffer);
                }
            }
        }

        public static void Save(ItemCollection items)
        {
            Drop.Clear();

            foreach (ListViewTemplate item in items)
            {
                Drop.Add(item.npcid, item.itemlist);
            }

            if (File.Exists(BinPath))
                File.Delete(BinPath);

            using (var file = File.Create(BinPath))
            {
                Serializer.SerializeWithLengthPrefix<Dictionary<int, List<long>>>(file, Drop, PrefixStyle.Fixed32);
            }
        }
    }
}
