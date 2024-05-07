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
    /// Interaction logic for InventorySiteWindow.xaml
    /// </summary>
    public partial class InventorySiteWindow : Window
    {
        // passed in
        public App myApp;
        private readonly MainWindow _parentWindow;
        private InventorySite _selectedInventorySite;

        // view control
        private bool isClosing = false;
        private bool shouldClose = true;

        // refernces to the campus data
        List<SupplyCount> supplyCounts;

        // new values changed by the window
        int bwCount { get; set; }
        int colorCount { get; set; }
        int tabloidBwCount { get; set; }
        int tabloidColorCount { get; set; }
        int paperTowelCount { get; set; }
        int tableSprayCount { get; set; }
        int wipesCount { get; set; }
        int sprayCanCount { get; set; }
        int stapleRefillsCount { get; set; }
        int tapeRefillsCount { get; set; }
        int clocksCount { get; set; }
        int bwLevel { get; set; }
        int colorLevel { get; set; }
        int tabloidBwLevel { get; set; }
        int tabloidColorLevel { get; set; }
        int paperTowelLevel { get; set; }
        int tableSprayLevel { get; set; }
        int wipesLevel { get; set; }
        int sprayCanLevel { get; set; }
        int stapleRefillsLevel { get; set; }
        int tapeRefillsLevel { get; set; }
        int clocksLevel { get; set; }

        public InventorySiteWindow(MainWindow parentWindow, InventorySite selectedInventorySite)
        {
            // Initialize New Window
            InitializeComponent();
            _parentWindow = parentWindow;
            _selectedInventorySite = selectedInventorySite;

            // Set Window Label
            windowLabel.Content = selectedInventorySite.Name;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // make refernces to data given from parent window
            supplyCounts = _parentWindow.supplyCounts;

            LoadCountsAndInitializeUI();
        }

        private void SetSupplyUI(Label countLabel, Slider levelSlider, SupplyType type)
        {
            var supplyCount = _parentWindow.supplyCounts.FirstOrDefault(s => s.SiteId == _selectedInventorySite.Id && s.Type == type);
            if (supplyCount != null)
            {
                countLabel.Content = supplyCount.Count.ToString();
                levelSlider.Value = supplyCount.Level;
            }
            else
            {
                countLabel.Content = "0";
                levelSlider.Value = 0;
            }
        }

        private void LoadCountsAndInitializeUI()
        {
            SetSupplyUI(bwCountLabel, bwCountSlider, SupplyType.BlackWhitePaper);
            SetSupplyUI(colorCountLabel, colorCountSlider, SupplyType.ColorPaper);
            SetSupplyUI(tabloidBwCountLabel, tabloidBwCountSlider, SupplyType.TabloidBlackWhitePaper);
            SetSupplyUI(tabloidColorCountLabel, tabloidColorCountSlider, SupplyType.TabloidColorPaper);
            SetSupplyUI(paperTowelCountLabel, paperTowelCountSlider, SupplyType.PaperTowel);
            SetSupplyUI(tableSprayCountLabel, tableSprayCountSlider, SupplyType.TableSpray);
            SetSupplyUI(wipesCountLabel, wipesCountSlider, SupplyType.Wipes);
            SetSupplyUI(sprayCanCountLabel, sprayCanCountSlider, SupplyType.SprayCan);
            SetSupplyUI(stapleRefillsCountLabel, stapleRefillsCountSlider, SupplyType.StaplerRefill);
            SetSupplyUI(tapeRefillsCountLabel, tapeRefillsCountSlider, SupplyType.TapeRefill);
            SetSupplyUI(clocksCountLabel, clocksCountSlider, SupplyType.Clock);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateOrCreateSupplyCount(bwCount, bwCountTextBox, bwCountSlider, SupplyType.BlackWhitePaper, bwCountLabel);
            UpdateOrCreateSupplyCount(colorCount, colorCountTextBox, colorCountSlider, SupplyType.ColorPaper, colorCountLabel);
            UpdateOrCreateSupplyCount(tabloidBwCount, tabloidBwCountTextBox, tabloidBwCountSlider, SupplyType.TabloidBlackWhitePaper, tabloidBwCountLabel);
            UpdateOrCreateSupplyCount(tabloidColorCount, tabloidColorCountTextBox, tabloidColorCountSlider, SupplyType.TabloidColorPaper, tabloidColorCountLabel);
            UpdateOrCreateSupplyCount(paperTowelCount, paperTowelCountTextBox, paperTowelCountSlider, SupplyType.PaperTowel, paperTowelCountLabel);
            UpdateOrCreateSupplyCount(tableSprayCount, tableSprayCountTextBox, tableSprayCountSlider, SupplyType.TableSpray, tableSprayCountLabel);
            UpdateOrCreateSupplyCount(wipesCount, wipesCountTextBox, wipesCountSlider, SupplyType.Wipes, wipesCountLabel);
            UpdateOrCreateSupplyCount(sprayCanCount, sprayCanCountTextBox, sprayCanCountSlider, SupplyType.SprayCan, sprayCanCountLabel);
            UpdateOrCreateSupplyCount(stapleRefillsCount, stapleRefillsCountTextBox, stapleRefillsCountSlider, SupplyType.StaplerRefill, stapleRefillsCountLabel);
            UpdateOrCreateSupplyCount(tapeRefillsCount, tapeRefillsCountTextBox, tapeRefillsCountSlider, SupplyType.TapeRefill, tapeRefillsCountLabel);
            UpdateOrCreateSupplyCount(clocksCount, clocksCountTextBox, clocksCountSlider, SupplyType.Clock, clocksCountLabel);

            // Update data in parent window
            SendDataToParent();
        }

        private void SendDataToParent()
        {
            _parentWindow.UpdateSupplyData(supplyCounts);
            MessageBox.Show("Inventory Submitted!");
        }

        private void UpdateOrCreateSupplyCount(int currentCount, TextBox countTextBox, Slider countSlider, SupplyType supplyType, Label countLabel)
        {
            var newCount = currentCount;
            if (!string.IsNullOrWhiteSpace(countTextBox.Text) && int.TryParse(countTextBox.Text, out int parsedCount))
            {
                newCount = parsedCount;
                countLabel.Content = newCount.ToString();
            }
            var newLevel = (int)countSlider.Value;
            int index = supplyCounts.FindIndex(s => s.SiteId == _selectedInventorySite.Id && s.Type == supplyType);

            if (index != -1)
            {
                supplyCounts[index].Count = newCount;
                supplyCounts[index].Level = newLevel;
            }
            else
            {
                SupplyCount newSupplyCount = new SupplyCount
                {
                    SiteId = _selectedInventorySite.Id,
                    Type = supplyType,
                    Count = newCount,
                    Level = newLevel
                };
                supplyCounts.Add(newSupplyCount);
            }
        }

        private void exit_menuItem_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.ExitProgram();
        }
    }
}
