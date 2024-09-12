using CustomerDistance_Calculator.Requets;
using CustomerDistance_Calculator.Services;
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
using System.Net.Http;
using System.Printing;
using Microsoft.Win32;
using CustomerDistance_Calculator.Factorys;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;

namespace CustomerDistance_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFactory _factory;
        private readonly IDistanceFileService _distanceFileService;

        private DataTable? _dataTable = null;

        public MainWindow()
        {
            InitializeComponent();
            saveFileBtn.IsEnabled = false;

            _factory = new DefaultFactory(); 
            _distanceFileService = new DistanceExcelFileService(_factory);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Datei|*.xlsx",
                Title = "Kundendatei auswählen",
            };
            if (openFileDialog.ShowDialog() != true)
                return;
            await LoadExcel(openFileDialog.FileName);
        }

        private async Task LoadExcel(string fileName)
        {
            _dataTable = await _distanceFileService.GetFileAsDataTable(fileName);
            dataGrid.ItemsSource = _dataTable.DefaultView;
            saveFileBtn.IsEnabled = true;
        }

        private async void SaveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_dataTable == null) return;
            saveFileBtn.IsEnabled = false;
            try
            {
                _dataTable = await _distanceFileService.UpdateDataTable(_dataTable, GetInts(originTB.Text), GetInts(destinationTB.Text), skipFirstRowCB.IsChecked ?? false);
            }
            catch (Exception)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten! Die Verarbeitung wurde abgebrochen!");
                return;
            }
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = _dataTable.DefaultView;
            MessageBoxResult dialogResult = MessageBox.Show("Soll die Änderungen gespeichert werden?", "Speichern?", MessageBoxButton.YesNo);
            if (dialogResult != MessageBoxResult.Yes)
                return;

            SaveFileDialog saveFileDialog = new ()
            {
                Filter = "Excel Datei|*.xlsx",
                Title = "Kundendatei speichern",
            };
            if (saveFileDialog.ShowDialog() != true)
                return;

            await _distanceFileService.SaveDataTable(saveFileDialog.FileName, _dataTable);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new(@"[^0-9;]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private static List<int> GetInts(string text)
        {
            List<int> ints = [];
            foreach (string s in text.Split(';'))
            {
                if (int.TryParse(s, out int i))
                    ints.Add(i);
            }
            return ints;
        }

    }
}