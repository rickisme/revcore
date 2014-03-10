using Data.Structures.World;
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

namespace BindPointEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<WorldPosition> _BindPointList = new List<WorldPosition>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindPointBuilder.Init();
            foreach (var item in BindPointBuilder.BindPoints.Values)
            {
                _BindPointList.AddRange(item);
            }
            DataListAdd();
        }

        private void DataListAdd()
        {
            DataListBox.Items.Clear();
            foreach (var position in _BindPointList)
            {
                DataListBox.Items.Add(position);
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var Position = new WorldPosition();
            _BindPointList.Add(Position);
            DataListAdd();
        }

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorldPosition position = (WorldPosition)DataListBox.SelectedItem;

            if (position == null)
                return;

            propertyGrid.SelectedObject = position;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            BindPointBuilder.Save(_BindPointList);
        }
    }
}
