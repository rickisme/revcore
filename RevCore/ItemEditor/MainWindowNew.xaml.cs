using Cryption;
using Data.Structures.Template.Item;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItemEditor
{
    /// <summary>
    /// Interaction logic for MainWindowNew.xaml
    /// </summary>
    public partial class MainWindowNew : Window
    {
        public MainWindowNew()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ItemBuilder.GetItemTemplate();
            RefreshList();
        }

        private void RefreshList()
        {
            DataListBox.ItemsSource = ItemBuilder.Items.ToList();
        }

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemTemplate view = (ItemTemplate)DataListBox.SelectedItem;

            if (view == null)
                return;

            propertyGrid.SelectedObject = view;
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = "YBi.cfg";
            fileDialog.Filter = "Yugang Data Info|YBi.cfg";

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                DataListBox.Items.Clear();
                string filename = fileDialog.FileName;
                ItemBuilder.YBBuffer = YBi.Decrypt(File.ReadAllBytes(filename));
                ItemBuilder.ProcessYBItemData();
                RefreshList();
            }
        }

        private void MenuItem_Click_Make(object sender, RoutedEventArgs e)
        {
            ItemBuilder.Save();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ItemTemplate addnew = new ItemTemplate();
            ItemBuilder.Items.Add(addnew);
            RefreshList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ItemBuilder.Save();
        }

        private void FilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterName.Text.Length == 0)
            {
                RefreshList();
            }
            else
            {
                var filterItems = ItemBuilder.Items.Where(v => v.Name.Contains(FilterName.Text)).ToList();
                DataListBox.ItemsSource = filterItems;
            }
        }

        private void FilterLevel_TextChanged(object sender, TextChangedEventArgs e)
        {
            int level = 0;
            try
            {
                level = (FilterLevel.Text.Length == 0) ? 0 : int.Parse(FilterLevel.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (FilterLevel.Text.Length == 0)
            {
                RefreshList();
            }
            else
            {
                var filterItems = ItemBuilder.Items.Where(v => v.Level >= level).ToList();
                DataListBox.ItemsSource = filterItems;
            }
        }
    }
}
