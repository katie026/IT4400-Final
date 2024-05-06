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
        public App myApp;
        private MainWindow _parentWindow;
        private ComputingSite _selectedComputingSite;

        List<Building> buildings;
        List<ComputingSite> computingSites;
        List<InventorySite> inventorySites;
        List<EquipmentItem> equipmentItems;
        List<SupplyCount> supplyCounts;

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
            buildings = _parentWindow.buildings;
            computingSites = _parentWindow.computingSites;
            inventorySites = _parentWindow.inventorySites;
            equipmentItems = _parentWindow.equipmentItems;
            supplyCounts = _parentWindow.supplyCounts;

            computersListBox.ItemsSource = buildings;
        }

        private void SendDataToParent()
        {
            _parentWindow.UpdateCampusData(buildings, computingSites, inventorySites, equipmentItems, supplyCounts);
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

        }

        private void computersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
