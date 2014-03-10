using Cryption;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DropsEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DropBuilder.GetDropTemplate();

            foreach (var data in DropBuilder.Drop)
            {
                ListViewTemplate view = new ListViewTemplate();
                view.npcid = data.Key;
                view.itemlist = data.Value;
                DataListBox.Items.Add(view);
            }
        }

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var view = (ListViewTemplate)DataListBox.SelectedItem;

            if (view == null)
                return;

            propertyGrid.SelectedObject = view;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewTemplate view = new ListViewTemplate();
            view.npcid = 0;
            view.itemlist = new List<long>();
            DataListBox.Items.Add(view);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DropBuilder.Save(DataListBox.Items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void YbDropOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = "*.cfg";
            fileDialog.Filter = "Yulgang Info|*.cfg";

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                DropBuilder.YBBuffer = YBi.Decrypt(File.ReadAllBytes(filename));
                DropBuilder.ReadYBData();
            }
        }
    }
}
