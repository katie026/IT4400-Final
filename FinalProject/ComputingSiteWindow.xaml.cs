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
    /// Interaction logic for ComputingSiteWindow.xaml
    /// </summary>
    public partial class ComputingSiteWindow : Window
    {
        // passed in
        public App myApp;
        private MainWindow _parentWindow;
        private ComputingSite _selectedComputingSite;

        // copies of the campus data
        List<Building> buildings;
        List<ComputingSite> computingSites;
        List<InventorySite> inventorySites;
        List<EquipmentItem> equipmentItems;
        List<SupplyCount> supplyCounts;

        // new values set by the window
        Building building;
        List<EquipmentItem> computers;
        List<EquipmentItem> printers;
        public List<CampusGroup> GroupList { get; set; }


        public ComputingSiteWindow(MainWindow parentWindow, ComputingSite selectedComputingSite)
        {
            // Initialize New Window
            InitializeComponent();
            _parentWindow = parentWindow;
            _selectedComputingSite = selectedComputingSite;

            // Set Window Label
            windowLabel.Content = selectedComputingSite.Name;
            // Set Campus Data
            SetData();
        }

        private void SetData()
        {
            // set data given from parent window
            buildings = _parentWindow.buildings;
            computingSites = _parentWindow.computingSites;
            inventorySites = _parentWindow.inventorySites;
            equipmentItems = _parentWindow.equipmentItems;
            supplyCounts = _parentWindow.supplyCounts;

            // populate Computers
            computers = equipmentItems
                .Where(item => item.SiteId == _selectedComputingSite.Id && (item.EquipmentType == EquipmentType.windowsComputer || item.EquipmentType == EquipmentType.macComputer))
                .ToList();
            computersListBox.ItemsSource = computers;

            // populate Printers
            printers = equipmentItems
                .Where(item => item.SiteId == _selectedComputingSite.Id && (item.EquipmentType == EquipmentType.blackWhitePrinter || item.EquipmentType == EquipmentType.colorPrinter))
                .ToList();
            printersListBox.ItemsSource = printers;

            // populate Buildings
            BuildingComboBox.ItemsSource = buildings;
            building = buildings.FirstOrDefault(item => item.Id == _selectedComputingSite.Building);
            if (building != null)
            {
                BuildingComboBox.SelectedItem = building;
            } else
            {
                if (buildings.Count > 0)
                {
                    BuildingComboBox.SelectedItem = buildings[0];
                } else
                {
                    BuildingComboBox.SelectedItem = null;
                }
            }

            // populate Groups
            // get all enum values
            GroupList = CampusGroup.GetValues(typeof(CampusGroup)).Cast<CampusGroup>().ToList();
            // bind GroupComboBox
            DataContext = this;
            UpdateGroup();
        }

        private void SendDataToParent()
        {
            // recombine equipment items
            equipmentItems = equipmentItems.Concat(computers).Concat(printers).Distinct().ToList();
            // update this site's information
            int buildingIndex = buildings.FindIndex(building => building.Id == _selectedComputingSite.Building);
            buildings[buildingIndex] = building; // update building and/or group
            int siteIndex = computingSites.FindIndex(site => site.Id == _selectedComputingSite.Id);

            _parentWindow.UpdateCampusData(buildings, computingSites, inventorySites, equipmentItems, supplyCounts);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGroup();
        }

        private void UpdateGroup()
        {
            if (building != null)
            {
                GroupComboBox.SelectedItem = building.Group;
                //MessageBox.Show($"Set group to {building.Group}");
            }
            else
            {
                GroupComboBox.SelectedItem = null;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SendDataToParent();
            myApp.RemoveWindow(this);
        }

        private void exit_menuItem_Click(object sender, RoutedEventArgs e)
        {
            SendDataToParent();
            _parentWindow.ExitProgram();
        }

        private void computersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedComputersCount = computersListBox.SelectedItems.Count;
            computersSelectedLabel.Content = $"Selected: {selectedComputersCount}";
        }

        private void computersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void printersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void printersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void submitChangesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newComputer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeComputers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cleanComputers_Click(object sender, RoutedEventArgs e)
        {
            // loop thorugh selected computers and count
            foreach (var selectedItem in computersListBox.SelectedItems)
            {
                // Do something with each selected item
                // For example, you can cast it to the appropriate type
                // and access its properties
                var listBoxItem = (EquipmentItem)selectedItem;
                // Or if you have a custom object bound to the ListBox
                // var item = (YourItemType)selectedItem;
                // Then you can access its properties
            }
        }

        private void newPrinter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removePrinters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void revertChangesButton_Click(object sender, RoutedEventArgs e)
        {
            SetData();
        }
    }
}
