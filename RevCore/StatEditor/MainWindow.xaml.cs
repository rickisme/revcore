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
using Data.Structures.Creature;

namespace StatEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ListViewTemplate> _list = new List<ListViewTemplate>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StatBuilder.GetStatTemplate();
            RefreshData();
            RefreshList();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewTemplate view = new ListViewTemplate();
            view.StrName = "Edit";
            view.Data = new CreatureBaseStats();
            _list.Add(view);

            RefreshList();
        }

        private void RefreshList()
        {
            StatBuilder.Stats = _list.Select(w => w.Data).ToList();
            DataListBox.ItemsSource = _list.ToList();
        }

        private void RefreshData()
        {
            foreach (var stat in StatBuilder.Stats)
            {
                ListViewTemplate view = new ListViewTemplate();
                view.StrName = string.Format("{0} - Lvl. {1}", stat.PlayerClass, stat.Level);
                view.Data = stat;
                _list.Add(view);
            }
        }

        private ListViewTemplate oldview;
        private int oldindex;

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewTemplate view = (ListViewTemplate)DataListBox.SelectedItem;

            if (view == null)
                return;

            if (oldview != null)
            {
                _list[oldindex].StrName = string.Format("{0} - Lvl. {1}", oldview.Data.PlayerClass, oldview.Data.Level);
                _list[oldindex].Data = oldview.Data;
                RefreshList();
            }

            oldview = view;
            oldindex = DataListBox.SelectedIndex;
            propertyGrid.SelectedObject = view.Data;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StatBuilder.Save();
        }
    }
}
