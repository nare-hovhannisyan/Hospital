using System;
using System.Windows;
using Wpf_UI.DataAccess;
using Wpf_UI.Models;

namespace Wpf_UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow window = new PatientsWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppointmentsWindow window = new AppointmentsWindow();
            window.Show();
            this.Close();
        }
    }
}