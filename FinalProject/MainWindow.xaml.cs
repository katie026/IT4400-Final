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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App myApp;
        private bool isClosing = false;
        private bool shouldClose = true;

        // deserialized data to be serialized
        // and filtered data to be displayed
        public List<Building> buildings;
        private List<Building> filteredBuildings;
        public List<ComputingSite> computingSites;
        private List<ComputingSite> filteredComputingSites;
        public List<InventorySite> inventorySites;
        private List<InventorySite> filteredInventorySites;
        public List<EquipmentItem> equipmentItems;
        private List<EquipmentItem> filteredEquipmentItems;
        public List<SupplyCount> supplyCounts;
        private List<SupplyCount> filteredSupplyCounts;

        // options
        public List<CampusGroup> GroupList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DeserializeData();

            // populate Groups
            // get all enum values
            GroupList = CampusGroup.GetValues(typeof(CampusGroup)).Cast<CampusGroup>().ToList();
            // bind GroupComboBox
            DataContext = this;
        }

        public void ExitProgram()
        {
            try
            {
                SerializeData();
                if (isClosing == true)
                {
                    // if window is currenlty closing due to window close
                    shouldClose = true;
                }
                else
                {
                    // if window is closing due to menu exit button
                    myApp.CloseAllWindows();
                }
            }
            catch (Exception ex)
            {
                // If serialization fails, warn the user
                MessageBoxResult result = MessageBox.Show($"Serialization failed: {ex.Message} Do you want to continue closing the application?",
                                                          "Serialization Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                // If the user clicks Yes, continue closing the application
                if (result == MessageBoxResult.Yes)
                {
                    if (isClosing == true)
                    {
                        shouldClose = true;
                    }
                    else
                    {
                        myApp.CloseAllWindows();
                    }
                }
                // If the user clicks No, cancel closing the application
                else if (result == MessageBoxResult.No)
                {
                    // do nothing if menu exit item close
                    // cancel closing event if Window Close
                    shouldClose = false;
                }
            }
        }

        private void DeserializeData()
        {
            // Deserialize the test data
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Assets\CampusData");
            DataSerializer serializer = new DataSerializer();
            var deserializedData = serializer.DeserializeData(filePath);

            buildings = deserializedData.Item1;
            computingSites = deserializedData.Item2;
            inventorySites = deserializedData.Item3;
            equipmentItems = deserializedData.Item4;
            supplyCounts = deserializedData.Item5;

            // Sort the lists
            SortLists();

            // Bind the ListBoxes to the lists
            BindLists();
        }

        private void SerializeData()
        {
            // Serialize the data back to the files
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Assets\CampusData");
            DataSerializer serializer = new DataSerializer();
            serializer.SerializeData(filePath, buildings, computingSites, inventorySites, equipmentItems, supplyCounts);
        }

        private void BindLists()
        {
            filteredBuildings = buildings.ToList();
            filteredComputingSites = computingSites.ToList();
            filteredInventorySites = inventorySites.ToList();

            buildingsListBox.ItemsSource = filteredBuildings;
            computingSitesListBox.ItemsSource = filteredComputingSites;
            inventorySitesListBox.ItemsSource = filteredInventorySites;
        }

        private void SortLists()
        {
            // BUILDINGS
            // sort by name and then by group
            buildings = buildings.OrderBy(building => building.Name).ThenBy(building => building.Group).ToList();

            // COMPUTING SITES
            // join operation to associate each computing site with its corresponding building
            var sortedComputingSites = from site in computingSites
                                       join building in buildings on site.Building equals building.Id
                                       orderby building.Group, site.Name
                                       select site;
            // Convert the result to a list
            computingSites = sortedComputingSites.ToList();

            // join operation to associate each inventory site with its corresponding building
            var sortedInventorySites = from site in inventorySites
                                       join building in buildings on site.Building equals building.Id
                                       orderby building.Group, site.Name
                                       select site;
            // Convert the result to a list
            inventorySites = sortedInventorySites.ToList();
        }

        public void UpdateCampusData(List<Building> buildings, List<ComputingSite> computingSites, List<InventorySite> inventorySites, List<EquipmentItem> equipmentItems, List<SupplyCount> supplyCounts)
        {
            this.buildings = buildings;
            this.computingSites = computingSites;
            this.inventorySites = inventorySites;
            this.equipmentItems = equipmentItems;
            this.supplyCounts = supplyCounts;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isClosing = false;
            myApp.CloseAllWindows();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isClosing = true;
            ExitProgram();
            if (!shouldClose)
            {
                // Cancel the closing event
                e.Cancel = true;
            } else
            {
                // continue closing event
                e.Cancel = false;
            }
        }

        private void exit_menuItem_Click(object sender, RoutedEventArgs e)
        {
            ExitProgram();
        }

        private void BuildingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // unfilter ComputingSites ListBox
            filteredComputingSites = computingSites.ToList();
            computingSitesListBox.ItemsSource = filteredComputingSites;
            // unfilter InventorySites ListBox
            filteredInventorySites = inventorySites.ToList();
            inventorySitesListBox.ItemsSource = filteredInventorySites;
        }

        private void ComputingSitesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InventorySitesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BuildingsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // check if an item is selected
            if (buildingsListBox.SelectedItem != null)
            {
                // get the selected item
                var selectedItem = (Building)buildingsListBox.SelectedItem;

                // filter ComputingSites ListBox
                filteredComputingSites = computingSites.Where(site => site.Building == selectedItem.Id).ToList();
                computingSitesListBox.ItemsSource = filteredComputingSites;
                // filter InventorySites ListBox
                filteredInventorySites = inventorySites.Where(site => site.Building == selectedItem.Id).ToList();
                inventorySitesListBox.ItemsSource = filteredInventorySites;
            }
        }

        private void ComputingSitesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // check if an item is selected
            if (computingSitesListBox.SelectedItem != null)
            {
                // get the selected item
                var selectedItem = (ComputingSite)computingSitesListBox.SelectedItem;

                // create new window
                ComputingSiteWindow newSiteWindow = new ComputingSiteWindow(this, selectedItem);
                // give it a pointer to myApp
                newSiteWindow.myApp = myApp;
                // add to list of windows
                myApp.AddWindow(newSiteWindow);  // remember window in list of windows
                // show window
                newSiteWindow.Show();
            }
        }

        private void InventorySitesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // check if an item is selected
            if (inventorySitesListBox.SelectedItem != null)
            {
                // get the selected item
                var selectedItem = (InventorySite)inventorySitesListBox.SelectedItem;

                // Perform the desired action with the selected item
                MessageBox.Show($"Double-clicked on: {selectedItem.Name}");
            }
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // check if an item is selected
            if (groupComboBox.SelectedItem != null)
            {
                // get the selected item
                var selectedItem = (CampusGroup)groupComboBox.SelectedItem;

                // Check if the selected item is different from the previously selected item
                if (selectedItem != CampusGroup.All)
                {
                    // filter Buildings ListBox
                    filteredBuildings = buildings.Where(building => building.Group == selectedItem).ToList();
                    buildingsListBox.ItemsSource = filteredBuildings;
                }
                else if (selectedItem == CampusGroup.All)
                {
                    // unfilter buildings
                    filteredBuildings = buildings;
                    buildingsListBox.ItemsSource = filteredBuildings;
                }
            }
        }
    }
}

public static class EnumExtensions
{
    public static List<string> GetEnumNames<T>()
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
}