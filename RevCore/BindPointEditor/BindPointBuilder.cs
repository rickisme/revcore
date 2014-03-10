using Data.Structures.World;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BindPointEditor
{
    public class BindPointBuilder
    {
        public static string BinPath = Path.GetFullPath("data/bindpoint.bin");

        public static Dictionary<int, List<WorldPosition>> BindPoints = new Dictionary<int, List<WorldPosition>>();

        public static void Init()
        {
            if (!File.Exists(BinPath))
            {
                using (var file = File.Create(BinPath))
                {
                    BindPoints = new Dictionary<int, List<WorldPosition>>();
                    Serializer.SerializeWithLengthPrefix<Dictionary<int, List<WorldPosition>>>(file, BindPoints, PrefixStyle.Fixed32);
                }
            }

            using (FileStream stream = File.OpenRead(BinPath))
            {
                BindPoints = Serializer.DeserializeWithLengthPrefix<Dictionary<int, List<WorldPosition>>>(stream, PrefixStyle.Fixed32);
            }
        }

        public static void Save(List<WorldPosition> list)
        {
            foreach (var item in list)
            {
                if (!BindPoints.ContainsKey(item.MapId))
                    BindPoints.Add(item.MapId, new List<WorldPosition>());

                BindPoints[item.MapId].Add(item);
            }

            if (File.Exists(BinPath))
                File.Delete(BinPath);

            using (var file = File.Open(BinPath, FileMode.Create))
            {
                Serializer.SerializeWithLengthPrefix<Dictionary<int, List<WorldPosition>>>(file, BindPoints, PrefixStyle.Fixed32);
            }
        }
    }
}
