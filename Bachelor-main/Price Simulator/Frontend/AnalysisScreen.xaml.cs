using Price_Simulator.Price_Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;
using Price_Simulator.Analyzer;
using Price_Simulator.Logger;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for AnalysisScreen.xaml
    /// </summary>
    public partial class AnalysisScreen : Window
    {
        private readonly ILogger[] _logs;
        public ObservableCollection<LogResult> LogResults { get; set; }
        public AnalysisScreen(ILogger[] logs)
        {
            _logs = logs;
            InitializeComponent();
            LogResults = new ObservableCollection<LogResult>();
            ResultsDataGrid.ItemsSource = LogResults;


            var results = LogAnalyzer.AnalyzeLogs(logs);  // Ensure AnalyzeLogs returns a collection of results

            LogResults.Clear();
            foreach (var result in results)
            {
                LogResults.Add(result);
            }

        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            var openFolderDialog = new OpenFolderDialog();

            openFolderDialog.ShowDialog();

            var folderLocation = openFolderDialog.FolderName;


            foreach (var log in _logs)
            {
                log.Dump(folderLocation);
            }
        }
    }
}

