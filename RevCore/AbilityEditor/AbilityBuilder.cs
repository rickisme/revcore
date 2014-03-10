using Data.Structures.SkillEngine;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AbilityEditor
{
    public class AbilityBuilder
    {
        public static string BinPath = Path.GetFullPath("data/abilities.bin");

        public static List<Ability> Abilities = new List<Ability>();

        public static void InitAbility()
        {
            if (!File.Exists(BinPath))
            {
                using (var file = File.Create(BinPath))
                {
                    Abilities = new List<Ability>();
                    Serializer.SerializeWithLengthPrefix<List<Ability>>(file, Abilities, PrefixStyle.Fixed32);
                }
            }

            using (FileStream stream = File.OpenRead(BinPath))
            {
                Abilities = Serializer.DeserializeWithLengthPrefix<List<Ability>>(stream, PrefixStyle.Fixed32);
            }
        }

        public static void Save()
        {
            if (File.Exists(BinPath))
                File.Delete(BinPath);

            using (var file = File.Open(BinPath, FileMode.Create))
            {
                Serializer.SerializeWithLengthPrefix<List<Ability>>(file, Abilities, PrefixStyle.Fixed32);
            }
        }
    }
}
