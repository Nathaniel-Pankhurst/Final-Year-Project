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
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings Currently not implemented");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Let's start a test!");
        }
    }
}
