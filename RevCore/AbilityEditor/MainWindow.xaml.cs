using Data.Enums.SkillEngine;
using Data.Structures.SkillEngine;
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

namespace AbilityEditor
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
            AbilityBuilder.InitAbility();
            DataListBox.ItemsSource = AbilityBuilder.Abilities;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AbilityBuilder.Save();
        }

        private void ReLoadButton_Click(object sender, RoutedEventArgs e)
        {
            AbilityBuilder.InitAbility();
            DataListBox.ItemsSource = AbilityBuilder.Abilities;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            Ability ability = new Ability()
            {
                AbilityId = 0,
                FirstLevel = 1.0,
                Step = 1.0,
                Time = 0
            };
            AbilityBuilder.Abilities.Add(ability);
            DataListBox.Items.Refresh();
            DataListBox.SelectedIndex = AbilityBuilder.Abilities.Count - 1;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) 
        {
            Ability ability = (Ability)DataListBox.SelectedItem;
            AbilityBuilder.Abilities.Remove(ability);
            DataListBox.Items.Refresh();
            DataListBox.SelectedIndex = AbilityBuilder.Abilities.Count - 1;
        }

        private void DataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ability ability = (Ability)DataListBox.SelectedItem;

            if (ability == null)
                return;

            propertyGrid.SelectedObject = ability;
        }
    }
}
