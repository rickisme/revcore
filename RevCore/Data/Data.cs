using Data.Structures.Creature;
using Data.Structures.SkillEngine;
using Data.Structures.Template;
using Data.Structures.Template.Item;
using Data.Structures.World;
using ProtoBuf;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Utilities;

namespace Data
{
    public class Data
    {
        public static string DataPath = Path.GetFullPath("data/");

        public static List<long> PlayerExperience = new List<long>();

        public static List<CreatureBaseStats> Stats = new List<CreatureBaseStats>();

        public static Dictionary<long, ItemTemplate> ItemTemplates = new Dictionary<long, ItemTemplate>();

        public static Dictionary<int, Skill> Skills = new Dictionary<int, Skill>();

        public static Dictionary<int, Ability> Abilities = new Dictionary<int, Ability>();

        public static Dictionary<int, MapTemplate> MapTemplates = new Dictionary<int, MapTemplate>();

        public static Dictionary<int, NpcTemplate> NpcTemplates = new Dictionary<int, NpcTemplate>();

        public static Dictionary<int, List<SpawnTemplate>> Spawns = new Dictionary<int, List<SpawnTemplate>>();

        public static Dictionary<int, List<WorldPosition>> BindPoints = new Dictionary<int, List<WorldPosition>>();

        public static Dictionary<int, List<long>> Drop = new Dictionary<int, List<long>>();

        protected delegate int Loader();

        protected static List<Loader> Loaders = new List<Loader>
                                                    {
                                                        LoadMapTemplates,
                                                        LoadPlayerExperience,
                                                        LoadBaseStats,
                                                        LoadItemTemplates,
                                                        LoadSpawns,
                                                        LoadBindPoints,
                                                        LoadNpcTemplates,
                                                        //LoadQuests,
                                                        LoadSkills,
                                                        LoadAbilities,
                                                        //LoadAbnormalities,
                                                        LoadDrop,
                                                        //LoadTeleports,
                                                        //CalculateNpcExperience,
                                                    };

        public static void LoadAll()
        {
            Parallel.For(0, Loaders.Count, i => LoadTask(Loaders[i]));
        }

        private static void LoadTask(Loader loader)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int readed = loader.Invoke();
            stopwatch.Stop();

            Log.Info("Data: {0,-26} {1,7} values in {2}s"
                , loader.Method.Name
                , readed
                , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
        }

        public static int LoadPlayerExperience()
        {
            PlayerExperience = new List<long>();

            using (FileStream fs = File.OpenRead(DataPath + "player_experience.bin"))
            {
                PlayerExperience = Serializer.DeserializeWithLengthPrefix<List<long>>(fs, PrefixStyle.Fixed32);
            }

            return PlayerExperience.Count;
        }

        public static int LoadMapTemplates()
        {
            MapTemplates = new Dictionary<int, MapTemplate>();

            using (FileStream stream = File.OpenRead(DataPath + "maps.bin"))
            {
                List<MapTemplate> templates = Serializer.DeserializeWithLengthPrefix<List<MapTemplate>>(stream, PrefixStyle.Fixed32);
                foreach (var template in templates)
                {
                    MapTemplates.Add(template.ID, template);
                }
            }

            return MapTemplates.Count;
        }

        public static int LoadBaseStats()
        {
            using (FileStream fs = File.OpenRead(DataPath + "stats.bin"))
            {
                Stats = Serializer.DeserializeWithLengthPrefix<List<CreatureBaseStats>>(fs, PrefixStyle.Fixed32);
            }

            return Stats.Count;
        }

        public static int LoadItemTemplates()
        {
            ItemTemplates = new Dictionary<long, ItemTemplate>();

            using (FileStream stream = File.OpenRead(DataPath + "items.bin"))
            {
                List<ItemTemplate> templates = Serializer.DeserializeWithLengthPrefix<List<ItemTemplate>>(stream, PrefixStyle.Fixed32);
                foreach (var template in templates)
                {
                    ItemTemplates.Add(template.Id, template);
                }
            }

            return ItemTemplates.Count;
        }

        public static int LoadSpawns()
        {
            Spawns = new Dictionary<int, List<SpawnTemplate>>();
            int readed = 0;

            foreach (string fileName in Directory.GetFiles(DataPath + "spawn"))
            {
                if (fileName.Contains("_spawn.bin"))
                {
                    using (FileStream stream = File.OpenRead(fileName))
                    {
                        List<SpawnTemplate> templates = Serializer.DeserializeWithLengthPrefix<List<SpawnTemplate>>(stream, PrefixStyle.Fixed32);
                        foreach (var template in templates)
                        {
                            if (!Spawns.ContainsKey(template.MapId))
                                Spawns.Add(template.MapId, new List<SpawnTemplate>());

                            Spawns[template.MapId].Add(template);
                            readed++;
                        }
                    }
                }
            }

            return readed;
        }

        public static int LoadNpcTemplates()
        {
            NpcTemplates = new Dictionary<int, NpcTemplate>();

            using (FileStream stream = File.OpenRead(DataPath + "npcs.bin"))
            {
                List<NpcTemplate> templates = Serializer.DeserializeWithLengthPrefix<List<NpcTemplate>>(stream, PrefixStyle.Fixed32);
                foreach (var template in templates)
                {
                    NpcTemplates.Add(template.ID, template);
                }
            }

            return NpcTemplates.Count;
        }

        public static int LoadSkills()
        {
            Skills = new Dictionary<int, Skill>();

            using (FileStream stream = File.OpenRead(DataPath + "skills.bin"))
            {
                List<Skill> templates = Serializer.DeserializeWithLengthPrefix<List<Skill>>(stream, PrefixStyle.Fixed32);
                foreach (var template in templates)
                {
                    Skills.Add(template.Id, template);
                }
            }

            return Skills.Count;
        }

        public static int LoadAbilities()
        {
            Abilities = new Dictionary<int, Ability>();
            int readed = 0;
            using (FileStream stream = File.OpenRead(DataPath + "abilities.bin"))
            {
                var list = Serializer.DeserializeWithLengthPrefix<List<Ability>>(stream, PrefixStyle.Fixed32);

                foreach (var ability in list)
                {
                    Abilities.Add(ability.AbilityId, ability);
                    readed++;
                }
            }

            return readed;
        }

        public static int LoadBindPoints()
        {
            BindPoints = new Dictionary<int, List<WorldPosition>>();
            //int readed = 0;

            using (FileStream stream = File.OpenRead(DataPath + "bindpoint.bin"))
            {
                BindPoints = Serializer.DeserializeWithLengthPrefix<Dictionary<int, List<WorldPosition>>>(stream, PrefixStyle.Fixed32);
            }

            return BindPoints.Count;
        }

        private static int LoadDrop()
        {
            Drop = new Dictionary<int, List<long>>();

            using (FileStream fs = File.OpenRead(DataPath + "drops.bin"))
            {
                Drop = Serializer.DeserializeWithLengthPrefix<Dictionary<int, List<long>>>(fs, PrefixStyle.Fixed32);
            }

            return Drop.Count;
        }
    }
}