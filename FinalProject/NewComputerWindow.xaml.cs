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
using System.Windows.Shapes;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for NewComputerWindow.xaml
    /// </summary>

    public partial class NewComputerWindow : Window
    {
        // passed in
        public App myApp;
        private readonly ComputingSiteWindow _parentWindow;
        List<string> types { get; set; }

        public NewComputerWindow(ComputingSiteWindow parentWindow)
        {
            // Initialize New Window
            InitializeComponent();
            _parentWindow = parentWindow;

            // Set Window Label
            windowLabel.Content = _parentWindow.selectedSite.Name;

            // Load the types
            LoadTypes();
        }

        private void LoadTypes()
        {
            types = new List<string>();
            types.Add("Windows PC");
            types.Add("MacOS Desktop");

            // Bind types to ComboBox
            typeComboBox.ItemsSource = types;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the selected type
            EquipmentType equipmentType;
            string selectedType = typeComboBox.SelectedItem as string;
            if (selectedType == "Windows PC")
            {
                equipmentType = EquipmentType.windowsComputer;
            } else
            {
                equipmentType = EquipmentType.macComputer;
            }

            // Retrieve the selected name
            string computerName = nameTextBox.Text;

            _parentWindow.AddComputer(equipmentType, computerName);
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the selected type
            string selectedType = typeComboBox.SelectedItem as string;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
