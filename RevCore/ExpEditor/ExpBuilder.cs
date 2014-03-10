using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExpEditor
{
    public class ExpBuilder
    {
        public static string BinPath = Path.GetFullPath("data/player_experience.bin");

        public static List<long> PlayerExperience = new List<long>();

        public static void GetData()
        {
            #region makefile if not exists
            for (int i = 0; i < 256; i++)
            {
                if (i == 0)
                {
                    PlayerExperience.Add(100);
                }
                else if (i <= 20)
                {
                    PlayerExperience.Add((long)(PlayerExperience[i - 1] * 1.4));
                }
                else if ((i > 20) && (i <= 40))
                {
                    PlayerExperience.Add((long)(PlayerExperience[i - 1] * 1.2));
                }
                else if ((i > 40) && (i <= 80))
                {
                    PlayerExperience.Add((long)(PlayerExperience[i - 1] * 1.1));
                }
                else if (i > 80)
                {
                    PlayerExperience.Add((long)(PlayerExperience[i - 1] * 1.05));
                }
            }

            if (!File.Exists(BinPath))
            {
                using (var file = File.Create(BinPath))
                {
                    Serializer.SerializeWithLengthPrefix<List<long>>(file, PlayerExperience, PrefixStyle.Fixed32);
                }
            }
            #endregion

            using (FileStream stream = File.OpenRead(BinPath))
            {
                PlayerExperience = Serializer.DeserializeWithLengthPrefix<List<long>>(stream, PrefixStyle.Fixed32);
            }
        }
    }
}
