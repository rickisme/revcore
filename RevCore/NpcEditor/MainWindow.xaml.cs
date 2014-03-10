using Data.Structures.Template;
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

namespace NpcEditor
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
            NpcBuilder.GetNpcTemplates();
            DataListBox.ItemsSource = NpcBuilder.NpcInfoTemplates;
        }

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NpcTemplate template = (NpcTemplate)DataListBox.SelectedItem;

            if (template == null)
                return;

            propertyGrid.SelectedObject = template;
        }
    }
}
