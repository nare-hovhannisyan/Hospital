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
using System.Data.Entity;
using static Wpf_UI.MainWindow;
using System.Transactions;

namespace Wpf_UI
{
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();
        }

        Appointment appointment = new Appointment();

        public void Clear()
        {
            txtPatientId.Text = txtDate.Text = txtHour.Text = txtType.Text = txtDiagnosis.Text = "";
            btnSave.Content = "Save";
            btnDelete.IsEnabled = false;
            appointment.Id = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateDataGrid();
        }

        void PopulateDataGrid()
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                dataGridAppointment.ItemsSource = db.Appointments.ToList<Appointment>();
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {

            appointment.PatientID = Int32.Parse(txtPatientId.Text.Trim());
            int time = Int32.Parse(txtHour.Text.Remove(2));
            int minute = Int32.Parse(txtHour.Text.Substring(3, 2));
            TimeSpan ts = new TimeSpan(time, minute, 0);
            DateTime result = txtDate.DisplayDate.Date + ts;
            appointment.Date = result;
            appointment.Type = txtType.Text;
            appointment.Diagnosis = txtDiagnosis.Text;

            
            using (HospitalDbContext db = new HospitalDbContext())
            {
                appointment.Patient = db.Patients.Where(x => x.Id == appointment.PatientID).FirstOrDefault();
                if (appointment.Patient ==null )
                {
                    MessageBox.Show("Wrong Patiend Id. Please insert valid ID");
                    txtPatientId.Text = "";
                }
                else
                {
                    if (appointment.Id == 0)
                        db.Appointments.Add(appointment);
                    else
                        db.Entry(appointment).State = EntityState.Modified;
                    Clear();
                    MessageBox.Show("Appointment submitted successfuly");
                    db.SaveChanges();
                    PopulateDataGrid();
                }
            };

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Appointment appointment1 = (Appointment)dataGridAppointment.SelectedItem;
            appointment.Id = Convert.ToInt32(appointment1.Id);
            using (HospitalDbContext db = new HospitalDbContext())
            {
                appointment = db.Appointments.Where(x => x.Id == appointment.Id).FirstOrDefault();
                txtPatientId.Text = appointment.PatientID.ToString();
                txtDate.Text = appointment.Date.ToString();
                txtType.Text = appointment.Type;
                txtDiagnosis.Text = appointment.Diagnosis;
            }
            btnSave.Content = "Update";
            btnDelete.IsEnabled = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "EF CRUD Operation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (HospitalDbContext db = new HospitalDbContext())
                {

                    var entry = db.Entry(appointment);
                    if (entry.State == EntityState.Detached)
                        db.Appointments.Attach(appointment);
                    db.Appointments.Remove(appointment);
                    db.SaveChanges();
                    PopulateDataGrid();
                    Clear();
                    MessageBox.Show("The item was deleted successfully!");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

    }
}