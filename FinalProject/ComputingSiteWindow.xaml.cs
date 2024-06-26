﻿using System;
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ComputingSiteWindow.xaml
    /// </summary>
    public partial class ComputingSiteWindow : Window
    {
        // passed in
        public App myApp;
        private readonly MainWindow _parentWindow;
        private ComputingSite _selectedComputingSite;

        // view control
        private bool isClosing = false;
        private bool shouldClose = true;

        // original campus data
        ComputingSite originalSelectedComputingSite;
        List<Building> originalBuildings;
        List<ComputingSite> originalComputingSites;
        List<InventorySite> originalInventorySites;
        List<EquipmentItem> originalEquipmentItems;
        List<SupplyCount> originalSupplyCounts;

        // refernces to the campus data
        List<Building> buildings;
        List<ComputingSite> computingSites;
        List<InventorySite> inventorySites;
        List<EquipmentItem> equipmentItems;
        List<SupplyCount> supplyCounts;

        // new values changed by the window
        Building building; // for reference
        public ComputingSite selectedSite; // properties can be changed
        List<EquipmentItem> computers; // properties can be changed
        List<EquipmentItem> printers; // properties can be changed
        public bool HasWhiteboard { get; set; }
        public bool HasBlackboard { get; set; }
        public bool HasPosterboard { get; set; }
        public bool HasInventory { get; set; }
        public bool HasClock { get; set; }

        public ComputingSiteWindow(MainWindow parentWindow, ComputingSite selectedComputingSite)
        {
            // Initialize New Window
            InitializeComponent();
            _parentWindow = parentWindow;
            _selectedComputingSite = selectedComputingSite;

            // Set Window Label
            windowLabel.Content = selectedComputingSite.Name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //CloneOriginalData();
            // make refernces to data given from parent window
            buildings = _parentWindow.buildings;
            computingSites = _parentWindow.computingSites;
            inventorySites = _parentWindow.inventorySites;
            equipmentItems = _parentWindow.equipmentItems;
            supplyCounts = _parentWindow.supplyCounts;
            selectedSite = _selectedComputingSite;

            HasWhiteboard = selectedSite.HasWhiteboard;
            HasBlackboard = selectedSite.HasBlackboard;
            HasPosterboard = selectedSite.HasPosterBoard;
            HasInventory = selectedSite.HasInventory;
            HasClock = selectedSite.HasClock;
            RefreshBindings();
        }

        public static T DeepClone<T>(T obj)
        {
            // use serialization for generic deep clone method
            // all objects must be serializable
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        private void CloneOriginalData()
        {
            originalBuildings = DeepClone(_parentWindow.buildings);
            originalComputingSites = DeepClone(_parentWindow.computingSites);
            originalInventorySites = DeepClone(_parentWindow.inventorySites);
            originalEquipmentItems = DeepClone(_parentWindow.equipmentItems);
            originalSupplyCounts = DeepClone(_parentWindow.supplyCounts);
            originalSelectedComputingSite = DeepClone(_selectedComputingSite);
        }

        private void RefreshBindings()
        {
            //SITE INFO
            DataContext = this;
            UpdateCheckboxStates();

            // COMPUTERS
            computers = equipmentItems
                .Where(item => item.SiteId == _selectedComputingSite.Id && (item.EquipmentType == EquipmentType.windowsComputer || item.EquipmentType == EquipmentType.macComputer))
                .ToList();
            computers = computers.OrderBy(computer => computer.Name).ThenBy(computer => computer.LastCleaned).ToList();
            computersListBox.ItemsSource = null;
            computersListBox.ItemsSource = computers;

            // PRINTERS
            printers = equipmentItems
                .Where(item => item.SiteId == _selectedComputingSite.Id && (item.EquipmentType == EquipmentType.blackWhitePrinter || item.EquipmentType == EquipmentType.colorPrinter))
                .ToList();
            printersListBox.ItemsSource = null;
            printersListBox.ItemsSource = printers;

            // BUILDINGS & GROUP
            buildingComboBox.ItemsSource= null;
            buildingComboBox.ItemsSource= buildings;
            building = buildings.FirstOrDefault(building => building.Id == selectedSite.Building);
            if (building != null)
            {
                buildingComboBox.SelectedItem = building;
                groupLabel.Content = building.Group;
            }
            else
            {
                buildingComboBox.SelectedItem = null;
                groupLabel.Content = "N/A";
            }
        }

        private void SendDataToParent()
        {
            // update equipment items
            equipmentItems = equipmentItems.Concat(computers).Concat(printers).Distinct().ToList();
            // update site information
            selectedSite.HasWhiteboard = HasWhiteboard;
            selectedSite.HasBlackboard = HasBlackboard;
            selectedSite.HasPosterBoard = HasPosterboard;
            selectedSite.HasInventory = HasInventory;
            selectedSite.HasClock = HasClock;
            int siteIndex = computingSites.FindIndex(site => site.Id == _selectedComputingSite.Id);
            computingSites[siteIndex] = selectedSite;

            _parentWindow.UpdateCampusData(buildings, computingSites, inventorySites, equipmentItems, supplyCounts);
        }

        private void ExitWindow()
        {
            try
            {
                SendDataToParent();
                if (isClosing == true)
                {
                    // if window is currenlty closing due to window close
                    shouldClose = true;
                }
                else
                {
                    // if window is closing due to menu exit button
                    myApp.RemoveWindow(this);
                }
            }
            catch (Exception ex)
            {
                // If serialization fails, warn the user
                MessageBoxResult result = MessageBox.Show($"Data was not saved: {ex.Message}. Do you want to continue closing the window?",
                                                          "Save Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                // If the user clicks Yes, continue closing the application
                if (result == MessageBoxResult.Yes)
                {
                    if (isClosing == true)
                    {
                        shouldClose = true;
                    }
                    else
                    {
                        myApp.RemoveWindow(this);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isClosing = true;
            ExitWindow();
            if (!shouldClose)
            {
                // Cancel the closing event
                e.Cancel = true;
            }
            else
            {
                // continue closing event
                e.Cancel = false;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isClosing = false;
        }

        private void exit_menuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendDataToParent();
                _parentWindow.ExitProgram();
            }
            catch (Exception ex)
            {
                // If serialization fails, warn the user
                MessageBoxResult result = MessageBox.Show($"Data was not saved: {ex.Message}. Do you want to continue closing the application?",
                                                          "Save Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                // If the user clicks Yes, continue closing the application
                if (result == MessageBoxResult.Yes)
                {
                    _parentWindow.ExitProgram();
                }
                // If the user clicks No, cancel closing the application
                else if (result == MessageBoxResult.No)
                {
                    // do nothing, let the user continue using the application
                }
            }
        }

        private void UpdateCheckboxStates()
        {
            // Update checkbox states based on the current properties
            DataContext = this;
            whiteboardCheckBox.IsChecked = HasWhiteboard;
            blackboardCheckBox.IsChecked = HasBlackboard;
            posterboardCheckBox.IsChecked = HasPosterboard;
            inventoryCheckBox.IsChecked = HasInventory;
            clockCheckBox.IsChecked = HasClock;
        }

        public void AddComputer(EquipmentType equipmentType, string name)
        {
            // add new computers
            computers.Add(new EquipmentItem(Guid.NewGuid().ToString(), selectedSite.Id, equipmentType, name));
            // sort
            computers = computers.OrderBy(computer => computer.Name).ThenBy(computer => computer.LastCleaned).ToList();
            // update binding
            computersListBox.ItemsSource = null;
            computersListBox.ItemsSource = computers;
        }
        public void AddPrinter(EquipmentType equipmentType, string name)
        {
            // add new computers
            printers.Add(new EquipmentItem(Guid.NewGuid().ToString(), selectedSite.Id, equipmentType, name));
            // sort
            printers = printers.OrderBy(printer => printer.Name).ThenBy(printer => printer.LastCleaned).ToList();
            // update binding
            printersListBox.ItemsSource = null;
            printersListBox.ItemsSource = printers;
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
            // create new window
            NewComputerWindow newWindow = new NewComputerWindow(this);
            // give it a pointer to myApp
            newWindow.myApp = this.myApp;
            // add to list of windows
            myApp.AddWindow(newWindow);  // remember window in list of windows
            // show window
            newWindow.Show();
        }

        private void removeComputers_Click(object sender, RoutedEventArgs e)
        {
            // If serialization fails, warn the user
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {computersListBox.SelectedItems.Count} computer(s)? This action cannot be undone.",
                                                      "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // If the user clicks Yes, continue closing the application
            if (result == MessageBoxResult.Yes)
            {
                // loop thorugh selected computers and remove from computers
                foreach (var selectedItem in computersListBox.SelectedItems)
                {
                    // get computer from selcted items
                    var selectedComputer = (EquipmentItem)selectedItem;
                    // delete in computers list
                    int index = computers.FindIndex(computer => computer.Id == selectedComputer.Id);
                    computers.RemoveAt(index);
                }
                // clear selection
                computersListBox.SelectedItems.Clear();
                // update binding
                computersListBox.ItemsSource = null;
                computersListBox.ItemsSource = computers;
            }
            // If the user clicks No, cancel closing the application
            else if (result == MessageBoxResult.No)
            {
                // do nothing
            }
        }

        private void cleanComputers_Click(object sender, RoutedEventArgs e)
        {
            // loop thorugh selected computers and update lastCleaned
            foreach (var selectedItem in computersListBox.SelectedItems)
            {
                // get computer from selcted items
                var selectedComputer = (EquipmentItem)selectedItem;
                // update in computers list
                int index = computers.FindIndex(computer => computer.Id == selectedComputer.Id);
                computers[index].LastCleaned = DateTime.Now;
            }
            computersListBox.SelectedItems.Clear();
            // update binding
            computersListBox.ItemsSource = null;
            computersListBox.ItemsSource = computers;
        }

        private void newPrinter_Click(object sender, RoutedEventArgs e)
        {
            // create new window
            NewPrinterWindow newWindow = new NewPrinterWindow(this);
            // give it a pointer to myApp
            newWindow.myApp = this.myApp;
            // add to list of windows
            myApp.AddWindow(newWindow);  // remember window in list of windows
            // show window
            newWindow.Show();
        }

        private void removePrinters_Click(object sender, RoutedEventArgs e)
        {
            // If serialization fails, warn the user
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {printersListBox.SelectedItems.Count} printer(s)? This action cannot be undone.",
                                                      "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // If the user clicks Yes, continue closing the application
            if (result == MessageBoxResult.Yes)
            {
                // loop thorugh selected computers and remove from computers
                foreach (var selectedItem in printersListBox.SelectedItems)
                {
                    // get computer from selcted items
                    var selectedPrinter = (EquipmentItem)selectedItem;
                    // delete in computers list
                    int index = printers.FindIndex(printer => printer.Id == selectedPrinter.Id);
                    printers.RemoveAt(index);
                }
                // clear selection
                printersListBox.SelectedItems.Clear();
                // update binding
                printersListBox.ItemsSource = null;
                printersListBox.ItemsSource = printers;
            }
            // If the user clicks No, cancel closing the application
            else if (result == MessageBoxResult.No)
            {
                // do nothing
            }
        }

        private void revertChangesButton_Click(object sender, RoutedEventArgs e)
        {
            buildings = originalBuildings;
            computingSites = originalComputingSites;
            inventorySites = originalInventorySites;
            equipmentItems = originalEquipmentItems;
            supplyCounts = originalSupplyCounts;
            selectedSite = originalSelectedComputingSite;

            RefreshBindings();
        }

        private void deselectComputersButton_Click(object sender, RoutedEventArgs e)
        {
            computersListBox.SelectedItems.Clear();
        }

        private void buildingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (buildingComboBox.SelectedItem != null)
            {
                // get building from selected items
                var selectedBuilding = (Building)buildingComboBox.SelectedItem;
                // update site's building
                selectedSite.Building = selectedBuilding.Id;
                // update local building
                building = selectedBuilding;
                // update group label
                groupLabel.Content = building.Group;
            }
        }

        private void whiteboardCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HasWhiteboard = true;
            whiteboardCheckBox.IsChecked = true;
        }

        private void whiteboardCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HasWhiteboard = false;
            whiteboardCheckBox.IsChecked = false; 
        }

        private void blackboardCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HasBlackboard = true;
            blackboardCheckBox.IsChecked = true;
        }

        private void blackboardCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HasBlackboard= false;
            blackboardCheckBox.IsChecked = false;
        }

        private void posterboardCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HasPosterboard = true;
            posterboardCheckBox.IsChecked = true;
        }

        private void posterboardCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HasPosterboard= false;
            posterboardCheckBox.IsChecked = false;
        }

        private void clockCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HasClock = true;
            clockCheckBox.IsChecked = true;
        }

        private void clockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HasClock= false;
            clockCheckBox.IsChecked = false;
        }

        private void inventoryCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HasInventory = true;
            inventoryCheckBox.IsChecked = true;
        }

        private void inventoryCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HasInventory= false;
            inventoryCheckBox.IsChecked = false;
        }
    }
}
