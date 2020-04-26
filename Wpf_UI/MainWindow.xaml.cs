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
using System.Data.SqlClient;
using System.Data.Entity;

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

        public class Patient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public DateTime DateofBirth { get; set; }
            public string Address { get; set; }
            public string TelephoneNumber { get; set; }

        }

        public class Appointment
        {
            public int Id { get; set; }
            public int PatientID { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public string Diagnosis { get; set; }
            public Patient Patient { get; set; }

        }

        public class HospitalDbContext : DbContext
        {
            public HospitalDbContext() : base("name=HospitalDbContext")
            {

            }
            public virtual DbSet<Patient> Patients { get; set; }
            public virtual DbSet<Appointment> Appointments { get; set; }
        }
    }
}