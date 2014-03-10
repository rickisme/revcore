using Cryption;
using Data.Structures.SkillEngine;
using Data.Structures.Template;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace SqlToBin
{
    class Program
    {
        public static string DataPath = Path.GetFullPath("data/");
        public static string EncodingName = "TIS-620";
        static void Main(string[] args)
        {
            Console.WriteLine("input 0 for SpawnTemplateExport");
            Console.WriteLine("input 1 for NpcTemplateExport");
            Console.WriteLine("input 2 for MapTemplateExport");
            Console.WriteLine("input 3 for SkillTemplateExport");
            Console.WriteLine();
            Console.Write("Input: ");
            var cmd = Console.ReadLine();

            int cmdid = int.Parse(cmd);

            switch (cmdid)
            {
                case 0: SpawnTemplateExport(); break;
                case 1: NpcTemplateExport(); break;
                case 2: MapTemplateExport(); break;
                case 3: SkillTemplateExport(); break;
            }
        }

        private static void SpawnTemplateExport()
        {
            List<SpawnTemplate> templates = new List<SpawnTemplate>();

            using (var entity = new PublicDbEntities())
            {
                var list = entity.TBL_XWWL_NPC_SL.ToList();
                foreach (var data in list)
                {
                    SpawnTemplate spawn = new SpawnTemplate()
                    {
                        NpcId = data.FLD_PID,
                        Type = (short)data.FLD_NPC,
                        MapId = data.FLD_MID,
                        X = (float)data.FLD_X,
                        Y = (float)data.FLD_Y,
                        Z = (float)data.FLD_Z,
                        Face1 = (float)data.FLD_FACE0,
                        Face2 = (float)data.FLD_FACE,
                    };
                    templates.Add(spawn);
                    Log.Debug("Npc SL NpcId = {0} Map = {1}", spawn.NpcId, spawn.MapId);
                }

                var list2 = entity.TBL_XWWL_NPC.ToList();
                foreach (var data in list2)
                {
                    SpawnTemplate spawn = new SpawnTemplate()
                    {
                        NpcId = data.FLD_PID,
                        Type = (short)data.FLD_NPC,
                        MapId = data.FLD_MID,
                        X = (float)data.FLD_X,
                        Y = (float)data.FLD_Y,
                        Z = (float)data.FLD_Z,
                        Face1 = (float)data.FLD_FACE0,
                        Face2 = (float)data.FLD_FACE,
                    };
                    templates.Add(spawn);
                    Log.Debug("Npc NpcId = {0} Map = {1}", spawn.NpcId, spawn.MapId);
                }

                using (var file = File.Create("Data/spawns.bin"))
                {
                    Serializer.SerializeWithLengthPrefix<List<SpawnTemplate>>(file, templates, PrefixStyle.Fixed32);
                }
            }
        }

        private static void NpcTemplateExport()
        {
            List<NpcTemplate> templates = new List<NpcTemplate>();

            int BytesOfSeparation = 7860;
            int ID_Offset = 0x0107b448;

            var YBiBuffer = YBi.Decrypt(File.ReadAllBytes(DataPath + "YBi.cfg"));

            List<byte[]> list = new List<byte[]>();
            lock (YBiBuffer)
            {
                while (BitConverter.ToInt32(YBiBuffer, ID_Offset) != 0)
                {
                    long itemId = BitConverter.ToInt64(YBiBuffer, ID_Offset);
                    byte[] temp = new byte[BytesOfSeparation];
                    Buffer.BlockCopy(YBiBuffer, ID_Offset, temp, 0, BytesOfSeparation);
                    list.Add(temp);
                    ID_Offset += BytesOfSeparation;
                }
            }

            lock (list)
            {
                try
                {
                    foreach (byte[] data in list)
                    {
                        lock (data)
                        {
                            using (MemoryStream Stream = new MemoryStream(data))
                            {
                                byte[] ids = new byte[4];
                                Stream.Read(ids, 0, ids.Length);

                                byte[] names = new byte[64];
                                Stream.Read(names, 0, names.Length);

                                Stream.Position += 2;

                                byte[] levels = new byte[2];
                                Stream.Read(levels, 0, levels.Length);

                                byte[] hps = new byte[4];
                                Stream.Read(hps, 0, hps.Length);

                                byte[] descs = new byte[1024];
                                Stream.Read(descs, 0, descs.Length);

                                byte[] choice1 = new byte[4];
                                Stream.Read(choice1, 0, choice1.Length);

                                byte[] choice2 = new byte[4];
                                Stream.Read(choice2, 0, choice2.Length);

                                byte[] choice3 = new byte[4];
                                Stream.Read(choice3, 0, choice3.Length);

                                byte[] choice4 = new byte[4];
                                Stream.Read(choice4, 0, choice4.Length);

                                int NpcId = BitConverter.ToInt32(ids, 0);
                                string NpcName = Encoding.GetEncoding(EncodingName).GetString(names).Replace("\0", "");
                                int NpcLevel = BitConverter.ToInt16(levels, 0);
                                int NpcHp = BitConverter.ToInt32(hps, 0);
                                string NpcDesc = Encoding.GetEncoding(EncodingName).GetString(descs).Replace("\0", "");
                                int NpcChoice1 = BitConverter.ToInt32(choice1, 0);
                                int NpcChoice2 = BitConverter.ToInt32(choice2, 0);
                                int NpcChoice3 = BitConverter.ToInt32(choice3, 0);
                                int NpcChoice4 = BitConverter.ToInt32(choice4, 0);

                                Log.Debug("-----------------------------------------------------");
                                Log.Debug("NpcId = {0}", NpcId);
                                Log.Debug("NpcName = {0}", NpcName);
                                Log.Debug("NpcLevel = {0}", NpcLevel);
                                Log.Debug("NpcHp = {0}", NpcHp);
                                Log.Debug("NpcDesc = {0}", NpcDesc);
                                Log.Debug("NpcChoice1 = {0}", NpcChoice1);
                                Log.Debug("NpcChoice2 = {0}", NpcChoice2);
                                Log.Debug("NpcChoice3 = {0}", NpcChoice3);
                                Log.Debug("NpcChoice4 = {0}", NpcChoice4);
                                Log.Debug("-----------------------------------------------------");

                                using (var entity = new PublicDbEntities())
                                {
                                    var dbdata = entity.TBL_XWWL_NPC.Where(v => v.FLD_PID == NpcId).FirstOrDefault();

                                    if (dbdata != null)
                                    {
                                        NpcTemplate template = new NpcTemplate()
                                        {
                                            ID = NpcId,
                                            Name = NpcName,
                                            HealthPoint = NpcHp,
                                            Attack = (int)dbdata.FLD_AT,
                                            Defense = (int)dbdata.FLD_DF,
                                            Npc = (NpcId < 10000 && NpcId > 0) ? 1 : 0,
                                            Level = (int)dbdata.FLD_LEVEL,
                                            Exp = (int)dbdata.FLD_EXP,
                                            Auto = (int)dbdata.FLD_AUTO,
                                            Boss = dbdata.FLD_BOSS,
                                        };

                                        templates.Add(template);

                                        Log.Debug("Npc NpcId = {0} Name = {1}", template.ID, template.Name);
                                    }
                                    else
                                    {
                                        Log.Debug("No db data for Npc id = {0}", NpcId);
                                    }
                                }


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorException("NpcTemplateExport:", ex);
                }
            }

            /*using (var file = File.Create("Data/npcs.bin"))
            {
                Serializer.SerializeWithLengthPrefix<List<NpcTemplate>>(file, templates, PrefixStyle.Fixed32);
            }*/
        }

        private static void MapTemplateExport()
        {
            List<MapTemplate> templates = new List<MapTemplate>();

            using (var entity = new PublicDbEntities())
            {
                var list = entity.TBL_XWWL_MAP.ToList();
                foreach (var data in list)
                {
                    MapTemplate map = new MapTemplate()
                    {
                        ID = data.FLD_MID,
                        Name = data.FLD_NAME,
                        File = data.FLD_FILE,
                        X = (int)data.X,
                        Y = (int)data.Y,
                    };
                    templates.Add(map);
                    Log.Debug("Map MapOd = {0} Name = {1}", map.ID, map.Name);
                }

                using (var file = File.Create("Data/maps.bin"))
                {
                    Serializer.SerializeWithLengthPrefix<List<MapTemplate>>(file, templates, PrefixStyle.Fixed32);
                }
            }
        }

        private static void SkillTemplateExport()
        {
            /*
            List<Skill> templates = new List<Skill>();

            using (var entity = new PublicDbEntities())
            {
                var list = entity.TBL_XWWL_KONGFU.ToList();
                foreach (var data in list)
                {
                    Skill skill = new Skill()
                    {
                        _ID = data.FLD_PID,
                        _GOE = (int)data.FLD_ZX,
                        _Job = (int)data.FLD_JOB,
                        _JobLevel = (int)data.FLD_JOBLEVEL,
                        _Level = (int)data.FLD_LEVEL,
                        _MaxLevel = (int)data.FLD_LEVEL,
                        _NeedManaPoint = (int)data.FLD_MP,
                        _NeedExperience = (int)data.FLD_NEEDEXP,
                        _Attack = (int)data.FLD_AT,
                        _Type = (int)data.FLD_TYPE,
                        _Effert = (int)data.FLD_EFFERT,
                        _Index = (int)data.FLD_INDEX,
                        _AttackCount = (int)data.FLD_攻击数量,
                        _Time = (int)data.FLD_TIME,
                    };
                    templates.Add(skill);
                    Log.Debug("Kongfu SkillId = {0}", skill._ID);
                }

                using (var file = File.Create("Data/skills.bin"))
                {
                    Serializer.SerializeWithLengthPrefix<List<Skill>>(file, templates, PrefixStyle.Fixed32);
                }
            }
             * */
        }
    }
}
