using Cryption;
using Data.Structures.Template.Item;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ItemEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /*public Dictionary<long, ItemTemplate> ItemDataDict = new Dictionary<long, ItemTemplate>();

        public byte[] YBiBuffer;

        public int BytesOfSeparation = 848;
        public int ID_Offset = 0x00000008;

        public string EncodingName = "big5";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ItemBuilder.GetItemTemplate();
            RefreshData();
            RefreshList();
        }

        private void RefreshList()
        {
            //ItemBuilder.Items = ItemDataDict.Select(w => w.Value).ToList();
            foreach (var item in ItemBuilder.Items)
            {
                //ItemListBox.Items.Insert(item.Key, item.Value.Name);
                ItemListBox.Items.Add(item.Name);
            }
            textbox_itemcount.Content = ItemBuilder.Items.Count.ToString();
        }

        private void RefreshData()
        {
            foreach (var stat in ItemBuilder.Items)
            {

            }
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = "YBi.cfg";
            fileDialog.Filter = "Yugang Data Info|YBi.cfg";

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                YBiBuffer = YBi.Decrypt(File.ReadAllBytes(filename));
                var list = Load();
                Process(list);
            }
        }

        private void MenuItem_Click_Make(object sender, RoutedEventArgs e)
        {
            ItemBuilder.Items = ItemDataDict.Select(w => w.Value).ToList();
            ItemBuilder.Save();
        }

        private void ItemListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = ItemListBox.SelectedIndex;
            ItemTemplate item;
            if (ItemDataDict.TryGetValue(index, out item))
            {
                textbox_itemid.Text = item.Id.ToString();
                textbox_itemname.Text = item.Name;
                textbox_itemlevel.Text = item.Level.ToString();
                textbox_itemreside1.Text = item.Resize1.ToString();
                textbox_itemreside2.Text = item.Resize2.ToString();
                textbox_itemweight.Text = item.Weight.ToString();
                textbox_itemmaxatk.Text = item.MaxAttack.ToString();
                textbox_itemminatk.Text = item.MinAttack.ToString();
                textbox_itemdef.Text = item.Defense.ToString();
                textbox_itemprice.Text = item.Price.ToString();
                textbox_itemdesc.Text = item.Desc;
            }

            if (ItemBuilder.Items[index] != null)
            {
                List<ItemTemplate> items = ItemBuilder.Items;
                textbox_itemid.Text = items[index].Id.ToString();
                textbox_itemname.Text = items[index].Name;
                textbox_itemlevel.Text = items[index].Level.ToString();
                textbox_itemreside1.Text = items[index].Resize1.ToString();
                textbox_itemreside2.Text = items[index].Resize2.ToString();
                textbox_itemweight.Text = items[index].Weight.ToString();
                textbox_itemmaxatk.Text = items[index].MaxAttack.ToString();
                textbox_itemminatk.Text = items[index].MinAttack.ToString();
                textbox_itemdef.Text = items[index].Defense.ToString();
                textbox_itemprice.Text = items[index].Price.ToString();
                textbox_itemdesc.Text = items[index].Desc;
            }
        }

        public List<byte[]> Load()
        {
            List<byte[]> list = new List<byte[]>();
            lock (YBiBuffer)
            {
                while (BitConverter.ToInt64(YBiBuffer, ID_Offset) != 0)
                {
                    long itemId = BitConverter.ToInt64(YBiBuffer, ID_Offset);
                    byte[] temp = new byte[BytesOfSeparation];
                    Buffer.BlockCopy(YBiBuffer, ID_Offset, temp, 0, BytesOfSeparation);
                    list.Add(temp);
                    ID_Offset += BytesOfSeparation;
                }
            }
            return list;
        }

        public void Process(List<byte[]> list)
        {
            lock (list)
            {
                try
                {
                    int index = 0;
                    ItemListBox.Items.Clear();
                    foreach (byte[] data in list)
                    {
                        lock (data)
                        {
                            byte[] byte_ItemId = new byte[4];
                            Buffer.BlockCopy(data, 0, byte_ItemId, 0, 4);

                            byte[] byte_ItemName = new byte[64];
                            Buffer.BlockCopy(data, 8, byte_ItemName, 0, 64);

                            byte byte_ItemZx = data[73];

                            byte[] byte_ItemReside1 = new byte[2];
                            Buffer.BlockCopy(data, 74, byte_ItemReside1, 0, 2);

                            byte[] byte_ItemLevel = new byte[2];
                            Buffer.BlockCopy(data, 76, byte_ItemLevel, 0, 2);

                            byte[] byte_ItemJobLevel = new byte[2];
                            Buffer.BlockCopy(data, 78, byte_ItemJobLevel, 0, 2);

                            byte byte_ItemSex = data[79];

                            byte[] byte_ItemReside2 = new byte[2];
                            Buffer.BlockCopy(data, 80, byte_ItemReside2, 0, 2);

                            byte[] byte_ItemWeight = new byte[2];
                            Buffer.BlockCopy(data, 82, byte_ItemWeight, 0, 2);

                            byte[] byte_ItemMaxAtk = new byte[2];
                            Buffer.BlockCopy(data, 84, byte_ItemMaxAtk, 0, 2);

                            byte[] byte_ItemMinAtk = new byte[2];
                            Buffer.BlockCopy(data, 86, byte_ItemMinAtk, 0, 2);

                            byte[] byte_ItemDef = new byte[2];
                            Buffer.BlockCopy(data, 88, byte_ItemDef, 0, 2);

                            byte byte_ItemNj = data[96];

                            byte[] byte_ItemPrice = new byte[4];
                            Buffer.BlockCopy(data, 100, byte_ItemPrice, 0, 4);

                            byte[] byte_ItemSellPrice = new byte[4];
                            Buffer.BlockCopy(data, 108, byte_ItemSellPrice, 0, 4);

                            byte[] byte_ItemEl = new byte[2];
                            Buffer.BlockCopy(data, 113, byte_ItemEl, 0, 2);

                            byte[] byte_ItemDesc = new byte[512];
                            Buffer.BlockCopy(data, 152, byte_ItemDesc, 0, 256);

                            byte[] byte_ItemWx = new byte[4];
                            Buffer.BlockCopy(data, 372, byte_ItemWx, 0, 4);

                            byte[] byte_ItemWxjd = new byte[4];
                            Buffer.BlockCopy(data, 376, byte_ItemWxjd, 0, 4);

                            int questItem = 0;
                            if (BitConverter.ToInt32(byte_ItemId, 0) > 900000001 && BitConverter.ToInt32(byte_ItemId, 0) < 1000000000)
                            {
                                questItem = 1;
                            }

                            int type = 0;
                            if (BitConverter.ToInt32(byte_ItemId, 0).ToString().Contains("1008000") || BitConverter.ToInt32(byte_ItemId, 0).ToString().Contains("16900") || BitConverter.ToInt32(byte_ItemId, 0).ToString().Contains("26900") || BitConverter.ToInt32(byte_ItemId, 0).ToString().Contains("1007000"))
                            {
                                type = 6;
                            }

                            int Magic = 0;
                            if (byte_ItemNj != 0)
                            {
                                string str = "2000000";
                                switch (byte_ItemNj.ToString().Length)
                                {
                                    case 1:
                                        str = "200000000";
                                        break;
                                    case 2:
                                        str = "20000000";
                                        break;
                                    case 4:
                                        str = "200000";
                                        break;
                                }
                                Magic = int.Parse(str + byte_ItemNj.ToString());
                            }

                            int Magic1 = Magic;
                            int Magic2 = 0;
                            int Magic3 = 0;
                            int Magic4 = 0;
                            int Magic5 = 0;
                            int Side = 0;
                            if (questItem == 1)
                            {
                                Side = 1;
                            }
                            else
                            {
                                Side = 0;
                            }

                            ItemTemplate item = new ItemTemplate()
                            {
                                Id = BitConverter.ToInt32(byte_ItemId, 0),
                                Name = Encoding.GetEncoding(EncodingName).GetString(byte_ItemName).Replace("\0", ""),
                                Resize1 = BitConverter.ToInt16(byte_ItemReside1, 0),
                                Level = BitConverter.ToInt16(byte_ItemLevel, 0),
                                Job_Level = BitConverter.ToInt16(byte_ItemJobLevel, 0),
                                Resize2 = BitConverter.ToInt16(byte_ItemReside2, 0),
                                Weight = BitConverter.ToInt16(byte_ItemWeight, 0),
                                MaxAttack = BitConverter.ToInt16(byte_ItemMaxAtk, 0),
                                MinAttack = BitConverter.ToInt16(byte_ItemMinAtk, 0),
                                Defense = BitConverter.ToInt16(byte_ItemDef, 0),
                                Price = BitConverter.ToInt32(byte_ItemPrice, 0),
                                Desc = Encoding.GetEncoding(EncodingName).GetString(byte_ItemDesc).Replace("\0", ""),
                                QuestItem = questItem,
                                Zx = byte_ItemZx,
                                Sex = byte_ItemSex,
                                Nj = byte_ItemNj,
                                El = BitConverter.ToInt16(byte_ItemEl, 0),
                                Wx = BitConverter.ToInt32(byte_ItemWx, 0),
                                Wxjd = BitConverter.ToInt32(byte_ItemWxjd, 0),
                                Type = type,
                                Side = Side,
                                Magic1 = Magic1,
                                Magic2 = Magic2,
                                Magic3 = Magic3,
                                Magic4 = Magic4,
                                Magic5 = Magic5,
                            };

                            if (!ItemDataDict.ContainsKey(item.Id))
                                ItemDataDict.Add(index, item);

                            ItemListBox.Items.Add(item.Name);
                        }
                        index++;
                    }
                    textbox_itemcount.Content = index.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("ItemData Process Error", ex);
                }
            }
        }*/
    }
}
