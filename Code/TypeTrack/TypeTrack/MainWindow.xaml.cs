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
using TypeTrack.TestModels;

namespace TypeTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITestController _testController;
        private ITestManager _testManager;
        private DispatcherTimer _UpdateTimer = new DispatcherTimer();
        private string _testString = string.Empty;
        private bool _clearEntryBox = false;
        private bool _mistakeMade = false;

        public MainWindow()
        {
            InitializeComponent();

            // Setup DispatchTimer Parameters
            _UpdateTimer.Interval = TimeSpan.FromSeconds(0.1);
            _UpdateTimer.Tick += _uiUpdateTimer_Tick;

            // Event Handlers
            StartButton.Click += StartButton_Click;
            SettingsButton.Click += SettingsButton_Click;

            _testManager = new TestManager();
            _testController = new TestController(_testManager);
            _testController.NextWord += _testController_NextWord;
            _testController.NewTest += _testController_NewTest;
            _testController.TestEnd += _testController_TestEnd;
            _testController.MistakeMade += _testController_MistakeMade;

            EntryBox.TextChanged += EntryBox_TextChanged;
        }

        private void _testController_MistakeMade(object sender, MistakeEventArgs e)
        {
            _mistakeMade = true;
        }

        private void _testController_TestEnd(object sender, TestEndEventArgs e)
        {
            _UpdateTimer.Stop();
            Dispatcher.Invoke(()=>{
                SetTestArea(string.Empty);
                EntryBox.Clear();
                TimeLabel.Content = "0:0";
            }); 
        }

        private void _uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            TestTelemetry testTelemetry = _testController.GetCurrentTelemetry();
            SetTestArea(_testString);

            if (_clearEntryBox)
            {
                EntryBox.Clear();
                _clearEntryBox = false;
            }
            if (_mistakeMade)
            {
                EntryBox.SelectionStart = EntryBox.Text.Length;
                EntryBox.SelectionLength = 0;
                _mistakeMade = false;
            }

            SpeedLabel.Content = string.Format("{0} WPM", testTelemetry.WPM);
            TimeLabel.Content = string.Format("{0}:{1}", testTelemetry.ElapsedTime.Minutes, testTelemetry.ElapsedTime.Seconds);
        }

        private void EntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool userProgress = false;
            if ((!string.IsNullOrEmpty(EntryBox.Text)) )
            {
                if (EntryBox.Text.Last() == ' ')
                {
                    EntryBox.Text = EntryBox.Text.Remove(EntryBox.Text.Length - 1);
                    userProgress = true;
                }
                else
                {
                    EntryBox.Text = EntryBox.Text;
                }
                _testController.UpdateUserEntryText(EntryBox.Text);
            }

            if (userProgress)
            {
                _testController.UserProgress();
            }
        }

        private void _testController_NewTest(object sender, WordEventArgs e)
        {
            _testString = e.RemainingWords;
            _UpdateTimer.Start();
        }

        private void _testController_NextWord(object sender, WordEventArgs e)
        {
            _testString = e.RemainingWords;
            _clearEntryBox = true;
        }

        private void SetTestArea(string testText)
        {
            SampleBlock.Text = testText;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings Currently not implemented");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _testController.StartNewTest(DefaultTexts\\Fox_Pangram);
        }
    }
}
