using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Price_Simulator.FileLoader;
using Price_Simulator.Logger;
using Price_Simulator.Price_Engine;

namespace Frontend
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private PriceEngine _priceEngine = new PriceEngine(new FileLoader(), new PriceEventFactory());

        private int _inputValue = 5; // Default starting value within the specified range

        public int InputValue
        {
            get => _inputValue;
            set => _inputValue = value;
        }

        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
        }

        private async void StartHighFrequency_Click(object sender, RoutedEventArgs e)
        {
            if (_inputValue > 30 || _inputValue < 5)
            {
                MessageBox.Show("Please enter a value between 5 and 30");
                return;
            }
            CancellationTokenSource cts = new CancellationTokenSource();
            var loadingScreen = new LoadingScreen();

            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select the location of your dll files";
            MessageBox.Show("please select the location of your dll files");
            openFolderDialog.ShowDialog();

            var folderLocation = openFolderDialog.FolderName;

            var loggers = new ILogger[2]; 
            try
            {
                Hide();
                loadingScreen.Show();
                cts.CancelAfter(_inputValue*1000);
                loggers = await _priceEngine.StartHighFrequencySimulation(folderLocation, cts.Token);
                var s = loggers.Length.ToString();
            }
            finally
            {
                loadingScreen.Close();
                Show();
            }

            var analysisScreen = new AnalysisScreen(loggers);

            analysisScreen.Show();
        }

        private async void StartLowFrequency_Click(object sender, RoutedEventArgs e)
        {
            if (_inputValue > 30 || _inputValue < 5)
            {
                MessageBox.Show("Please enter a value between 5 and 30");
                return;
            }
            CancellationTokenSource cts = new CancellationTokenSource();
            var loadingScreen = new LoadingScreen();

            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select the location of your dll files";
            MessageBox.Show("please select the location of your dll files");
            openFolderDialog.ShowDialog();

            var folderLocation = openFolderDialog.FolderName;

            var loggers = new ILogger[2];
            try
            {
                Hide();
                loadingScreen.Show();
                cts.CancelAfter(_inputValue*1000);
                loggers = await _priceEngine.StartLowFrequencySimulation(folderLocation, cts.Token);
                var s = loggers.Length.ToString();
            }
            finally
            {
                loadingScreen.Close();
                Show();
            }

            var analysisScreen = new AnalysisScreen(loggers);

            analysisScreen.Show();
        }

        private async void StartBurstFrequency_Click(object sender, RoutedEventArgs e)
        {
            if (_inputValue > 30 || _inputValue < 5)
            {
                MessageBox.Show("Please enter a value between 5 and 30");
                return;
            }
            CancellationTokenSource cts = new CancellationTokenSource();
            var loadingScreen = new LoadingScreen();

            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select the location of your dll files";
            MessageBox.Show("please select the location of your dll files");
            openFolderDialog.ShowDialog();

            var folderLocation = openFolderDialog.FolderName;

            var loggers = new ILogger[2];
            try
            {
                Hide();
                loadingScreen.Show();
                cts.CancelAfter(_inputValue * 1000);
                loggers = await _priceEngine.StartBurstFrequencySimulation(folderLocation, cts.Token);
                var s = loggers.Length.ToString();
            }
            finally
            {
                loadingScreen.Close();
                Show();
            }

            var analysisScreen = new AnalysisScreen(loggers);

            analysisScreen.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}