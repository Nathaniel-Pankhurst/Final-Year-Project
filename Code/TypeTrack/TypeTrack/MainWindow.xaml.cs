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
using TypeTrack.Controllers;

namespace TypeTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestController _testController;

        public MainWindow()
        {
            InitializeComponent();

            // Event Handlers
            StartButton.Click += StartButton_Click;
            SettingsButton.Click += SettingsButton_Click;

            _testController = new TestController(this.EntryBox.Text);
            _testController.NextWord += _testController_NextWord;
            _testController.NewTest += _testController_NewTest;

            EntryBox.TextChanged += EntryBox_TextChanged;
        }

        private void EntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryBox.Text.Last() == ' ')
            {
                EntryBox.Text = EntryBox.Text.Substring(EntryBox.Text.Length - 1);
                _testController.UserProgress();
            }
        }

        private void _testController_NewTest(object sender, WordEventArgs e)
        {
            SetTestArea(e.RemainingWords);
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
