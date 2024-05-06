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

        public List<Building> buildings;
        public List<ComputingSite> computingSites;
        public List<InventorySite> inventorySites;
        public List<EquipmentItem> equipmentItems;
        public List<SupplyCount> supplyCounts;

        public MainWindow()
        {
            InitializeComponent();
            DeserializeData();
        }

        public void ExitProgram()
        {
            try
            {
                SerializeData();
            }
            catch (Exception ex)
            {
                // If serialization fails, warn the user
                MessageBoxResult result = MessageBox.Show($"Serialization failed: {ex.Message}. Do you want to continue closing the application?",
                                                          "Serialization Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                // If the user clicks Yes, continue closing the application
                if (result == MessageBoxResult.Yes)
                {
                    myApp.CloseAllWindows();
                }
                // If the user clicks No, cancel closing the application
                else if (result == MessageBoxResult.No)
                {
                    // do nothing, let the user continue using the application
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

            // Sort the personList
            SortLists();

            // Bind the ListBoxes to the lists
            buildingsListBox.ItemsSource = buildings;
            computingSitesListBox.ItemsSource = computingSites;
            inventorySitesListBox.ItemsSource = inventorySites;
        }

        private void SerializeData()
        {
            // Serialize the data back to the files
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Assets\CampusData");
            DataSerializer serializer = new DataSerializer();
            serializer.SerializeData(filePath, buildings, computingSites, inventorySites, equipmentItems, supplyCounts);
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
            // in case the window was closed using a window control
            ExitProgram();
        }

        private void exit_menuItem_Click(object sender, RoutedEventArgs e)
        {
            ExitProgram();
        }

        private void BuildingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

                // Perform the desired action with the selected item
                MessageBox.Show($"Double-clicked on: {selectedItem.Name}");
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
    }
}


//private void SerializeTestData()
//{
//    // Creating test data for Buildings
//    List<Building> buildings = new List<Building>
//            {
//                new Building
//                {
//                    Id = "1",
//                    Name = "Library A",
//                    Address = new Address
//                    {
//                        Street = "123 Main St",
//                        City = "Cityville",
//                        State = "Stateville",
//                        StateCode = "ST",
//                        Country = "Countryville",
//                        ZipCode = "12345"
//                    },
//                    Longitude = 123.456f,
//                    Latitude = 78.910f,
//                    Group = CampusGroup.G1,
//                    Type = BuildingType.Library
//                },
//                new Building
//                {
//                    Id = "2",
//                    Name = "Residential Hall B",
//                    Address = new Address
//                    {
//                        Street = "456 Elm St",
//                        City = "Townsville",
//                        State = "Stateton",
//                        StateCode = "SS",
//                        Country = "Countryland",
//                        ZipCode = "67890"
//                    },
//                    Longitude = 234.567f,
//                    Latitude = 89.012f,
//                    Group = CampusGroup.R1,
//                    Type = BuildingType.ResidentialHall
//                }
//            };

//    // Creating test data for ComputingSites
//    List<ComputingSite> computingSites = new List<ComputingSite>
//            {
//                new ComputingSite
//                {
//                    Id = "1b",
//                    Building = "1",
//                    Name = "Computer Lab 1",
//                    Type = ComputingSiteType.Classroom,
//                    ChairCount = 30,
//                    HasClock = true,
//                    HasWhiteboard = true,
//                    HasBlackboard = false,
//                    HasPosterBoard = false,
//                    HasInventory = true,
//                    NearestInventoryId = "1",
//                    SiteCaptainUserId = "1001"
//                },
//                new ComputingSite
//                {
//                    Id = "2a",
//                    Building = "2",
//                    Name = "Printer Station 1",
//                    Type = ComputingSiteType.PrinterOnly,
//                    ChairCount = 0,
//                    HasClock = false,
//                    HasWhiteboard = false,
//                    HasBlackboard = false,
//                    HasPosterBoard = false,
//                    HasInventory = false,
//                    NearestInventoryId = "0",
//                    SiteCaptainUserId = "1002"
//                }
//            };

//    // Creating test data for InventorySites
//    List<InventorySite> inventorySites = new List<InventorySite>
//            {
//                new InventorySite
//                {
//                    Id = "1b",
//                    Building = "1",
//                    Name = "Supply Closet A",
//                    Types = new InventorySiteType[] { InventorySiteType.Closet }
//                },
//                new InventorySite
//                {
//                    Id = "2b",
//                    Building = "2",
//                    Name = "Kitchenette B",
//                    Types = new InventorySiteType[] { InventorySiteType.CornellKitchenette }
//                }
//            };

//    // Creating test data for EquipmentItems
//    List<EquipmentItem> equipmentItems = new List<EquipmentItem>
//            {
//                new EquipmentItem
//                {
//                    Id = "1",
//                    SiteId = "1",
//                    EquipmentType = EquipmentType.windowsComputer,
//                    Name = "Computer1",
//                    LastCleaned = DateTime.Now.AddDays(-10),
//                    ConnectedComputerId = "0"
//                },
//                new EquipmentItem
//                {
//                    Id = "2",
//                    SiteId = "2",
//                    EquipmentType = EquipmentType.blackWhitePrinter,
//                    Name = "Printer1",
//                    LastCleaned = DateTime.Now.AddDays(-5),
//                    ConnectedComputerId = "1"
//                }
//            };

//    // Creating test data for SupplyCounts
//    List<SupplyCount> supplyCounts = new List<SupplyCount>
//            {
//                new SupplyCount
//                {
//                    SiteId = "1",
//                    Type = SupplyType.BlackWhitePaper,
//                    Count = 1000,
//                    Level = 80
//                },
//                new SupplyCount
//                {
//                    SiteId = "1",
//                    Type = SupplyType.ColorPaper,
//                    Count = 500,
//                    Level = 60
//                }
//            };

//    // Serialize the test data
//    string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Assets\CampusData");
//    DataSerializer serializer = new DataSerializer();

//    serializer.SerializeData(filePath, buildings, computingSites, inventorySites, equipmentItems, supplyCounts);

//    // Deserialize the test data
//    var deserializedData = serializer.DeserializeData(filePath);
//    // Access and work with the deserialized data as needed
//}