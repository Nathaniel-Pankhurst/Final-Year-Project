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
using System.Windows.Threading;
using TypeTrack.Controllers;

namespace TypeTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestController _testController;
        private DispatcherTimer _uiUpdateTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            // Setup DispatchTimer Parameters
            _uiUpdateTimer.Interval = TimeSpan.FromSeconds(0.1);
            _uiUpdateTimer.Tick += _uiUpdateTimer_Tick;

            // Event Handlers
            StartButton.Click += StartButton_Click;
            SettingsButton.Click += SettingsButton_Click;

            _testController = new TestController();
            _testController.NextWord += _testController_NextWord;
            _testController.NewTest += _testController_NewTest;
            _testController.TestEnd += _testController_TestEnd;

            EntryBox.TextChanged += EntryBox_TextChanged;
        }

        private void _testController_TestEnd(object sender, TestEndEventArgs e)
        {
            _uiUpdateTimer.Stop();
            Dispatcher.Invoke(()=>{
                SetTestArea(string.Empty);
                TimeLabel.Content = "0:0";
            }); 
        }

        private void _uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            TestTelemetry testTelemetry = _testController.GetCurrentTelemetry();
            SpeedLabel.Content = string.Format("{0} WPM", testTelemetry.WPM);
            TimeLabel.Content = string.Format("{0}:{1}", testTelemetry.ElapsedTime.Minutes, testTelemetry.ElapsedTime.Seconds);
        }

        private void EntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryBox.Text.Last() == ' ')
            {
                string userEntryString = EntryBox.Text.Substring(EntryBox.Text.Length - 1);
                if (!string.IsNullOrEmpty(userEntryString))
                {
                    _testController.UserProgress(userEntryString);
                }
            }
        }

        private void _testController_NewTest(object sender, WordEventArgs e)
        {
            SetTestArea(e.RemainingWords);
            _uiUpdateTimer.Start();
        }

        private void _testController_NextWord(object sender, WordEventArgs e)
        {
            SetTestArea(e.RemainingWords);
        }

        private void SetTestArea(string testText)
        {
            SampleBlock.Text = testText;
            EntryBox.Clear();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings Currently not implemented");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _testController.StartNewTest();
        }
    }
}
