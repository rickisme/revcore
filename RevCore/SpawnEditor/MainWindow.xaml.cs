using Data.Structures.Template;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpawnEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, string> Names = new Dictionary<int, string>();

        private List<ListViewTemplate> _listview = new List<ListViewTemplate>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Names = SpawnBuilder.GetNames();
        }

        private void RefreshListBox()
        {
            //SpawnBuilder.SpawnTemplates = _list.Select(w => w.Data).ToList();
            DataListBox.Items.Clear();

            foreach (var view in _listview)
            {
                DataListBox.Items.Add(view);
            }
        }

        private ListViewTemplate oldview;

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = (ListBox)sender;

            ListViewTemplate view = (ListViewTemplate)listbox.SelectedItem;

            if (view == null)
                return;

            if (oldview != null)
            {
                string name = GetNpcName(oldview.Data.NpcId);

                _listview
                    .Where(v => v.Data.NpcId == oldview.Data.NpcId)
                    .First()
                    .Name = name;

                listbox.Items.Refresh();
            }

            oldview = view;
            propertyGrid.SelectedObject = view.Data;
        }

        private string GetNpcName(int npcId)
        {
            string name = string.Empty;
            Names.TryGetValue(npcId, out name);
            return (name == null) ? npcId.ToString() : name;
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            DataListBox.ItemsSource = null;
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            _listview.Clear();
            DataListBox.Items.Clear();

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = "*.bin";
            fileDialog.Filter = "Yugang Data Binary|*.bin";

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                var templates = SpawnBuilder.GetSpawnTemplates(filename);
                foreach (var temp in templates)
                {
                    _listview.Add(new ListViewTemplate()
                    {
                        Name = GetNpcName(temp.NpcId),
                        Data = temp
                    });
                }
                RefreshListBox();
            }
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.DefaultExt = "*.bin";
            fileDialog.Filter = "Yugang Data Binary|*.bin";

            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                SpawnBuilder.SaveToBin(filename, _listview.Select(v => v.Data).ToList());
            }
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewTemplate view = new ListViewTemplate();
            view.Name = "N/A";
            view.Data = new SpawnTemplate();
            _listview.Add(view);
            RefreshListBox();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListViewTemplate)DataListBox.SelectedItem;

            if (item == null)
                return;

            _listview.Remove(item);
            RefreshListBox();
        }
    }
}
