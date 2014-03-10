using System;
using System.Collections.Generic;
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

namespace ExpEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ListViewTemplate> _lists = new List<ListViewTemplate>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExpBuilder.GetData();

            for (int i = 0; i < ExpBuilder.PlayerExperience.Count; i++)
            {
                ListViewTemplate template = new ListViewTemplate()
                {
                    Level = i,
                    exp = ExpBuilder.PlayerExperience[i]
                };

                _lists.Add(template);
            }

            DataListBox.ItemsSource = _lists;
        }

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewTemplate view = (ListViewTemplate)DataListBox.SelectedItem;

            if (view == null)
                return;

            propertyGrid.SelectedObject = view;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
