using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data.Structures.Creature;
using ProtoBuf;
using Data.Structures.Template.Item;
using System;
using Utilities;
using System.Text;

namespace ItemEditor
{
    public class ItemBuilder
    {
        public static string BinPath = Path.GetFullPath("data/items.bin");

        public static List<ItemTemplate> Items = new List<ItemTemplate>();

        public static string EncodingName = "TIS-620";
        public static byte[] YBBuffer;
        public static int BytesOfSeparation = 848;
        public static int ID_Offset = 0x00000008;
        public static List<byte[]> ItemBufferList = new List<byte[]>();

        public static void GetItemTemplate()
        {
            if (!File.Exists(BinPath))
            {
                using (var file = File.Create(BinPath))
                {
                    Serializer.SerializeWithLengthPrefix<List<ItemTemplate>>(file, Items, PrefixStyle.Fixed32);
                }
            }

            using (FileStream stream = File.OpenRead(BinPath))
            {
                Items = Serializer.DeserializeWithLengthPrefix<List<ItemTemplate>>(stream, PrefixStyle.Fixed32).ToList();
            }
        }

        public static void ProcessYBItemData()
        {
            lock (YBBuffer)
            {
                while (BitConverter.ToInt64(YBBuffer, ID_Offset) != 0)
                {
                    long itemId = BitConverter.ToInt64(YBBuffer, ID_Offset);
                    byte[] temp = new byte[BytesOfSeparation];
                    Buffer.BlockCopy(YBBuffer, ID_Offset, temp, 0, BytesOfSeparation);
                    ItemBufferList.Add(temp);
                    ID_Offset += BytesOfSeparation;
                }
            }

            try
            {
                foreach (byte[] data in ItemBufferList)
                {
                    using (MemoryStream Stream = new MemoryStream(data))
                    {
                        using (BinaryReader Reader = new BinaryReader(Stream))
                        {
                            ItemTemplate template = new ItemTemplate();

                            template.Id = Reader.ReadInt32();
                            Reader.ReadInt32();
                            template.Name = Encoding.GetEncoding(EncodingName).GetString(Reader.ReadBytes(64)).Replace("\0", "");
                            Reader.ReadByte();
                            template.Side = Reader.ReadByte();
                            template.Class = Reader.ReadByte();
                            Reader.ReadByte();
                            template.Level = Reader.ReadInt16();
                            template.JobLevel = Reader.ReadByte();
                            template.Gender = Reader.ReadByte();
                            template.Category = Reader.ReadByte();
                            template.SubCategory = Reader.ReadByte();
                            template.Weight = Reader.ReadInt16();
                            template.MaxAttack = Reader.ReadInt16();
                            template.MinAttack = Reader.ReadInt16();
                            template.Defense = Reader.ReadInt16();
                            template.Accuracy = Reader.ReadInt16();
                            Reader.ReadInt64();
                            template.Price = Reader.ReadInt32();
                            Reader.ReadInt64();
                            Reader.ReadInt64();
                            Reader.ReadInt64();
                            template.Description = Encoding.GetEncoding(EncodingName).GetString(Reader.ReadBytes(256)).Replace("\0", "");

                            Items.Add(template);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("ProcessYBItemData:", ex);
            }
        }

        public static void Save()
        {
            if (File.Exists(BinPath))
                File.Delete(BinPath);

            using (var file = File.Open(BinPath, FileMode.Create))
            {
                Serializer.SerializeWithLengthPrefix<List<ItemTemplate>>(file, Items, PrefixStyle.Fixed32);
            }
        }
    }
}
