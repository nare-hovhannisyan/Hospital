using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Wpf_UI.DataAccess;
using Wpf_UI.Models;

namespace Wpf_UI
{
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();
        }

        Appointment appointment = new Appointment();

        private void Clear()
        {
            txtPatientId.Text = txtDate.Text = txtStartTime.Text = txtEndTime.Text = txtType.Text = txtDiagnosis.Text = "";
            btnSave.Content = "Save";
            btnDelete.IsEnabled = false;
            appointment.Id = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                dataGridAppointment.ItemsSource = db.Appointments.ToList<Appointment>();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if(!int.TryParse(txtPatientId.Text.Trim(), out id))
            {
                MessageBox.Show("Patient Id is not valid. Please insert a valid ID");
                txtPatientId.Text = "";
                return;
            }           
            appointment.PatientID = id;


            TimeSpan startTime, endTime;
            if ((!TimeSpan.TryParse(txtStartTime.Text, out startTime)) | (!TimeSpan.TryParse(txtEndTime.Text, out endTime)))
            {
                MessageBox.Show("Please insert valid hours eg. 09:00");
                txtStartTime.Text = "";
                txtEndTime.Text = "";
                return;
            }
            appointment.Date = txtDate.DisplayDate.Date;
            appointment.Start = startTime;
            appointment.End = endTime;
            appointment.Type = txtType.Text;
            appointment.Diagnosis = txtDiagnosis.Text;


            using (HospitalDbContext db = new HospitalDbContext())
            {
                appointment.Patient = db.Patients.FirstOrDefault(x => x.Id == appointment.PatientID);

                if (appointment.Patient == null)
                {
                    MessageBox.Show("Incorrect Patient ID");
                    txtPatientId.Text = "";
                } 
                else if(endTime <startTime)
                {
                    MessageBox.Show("Ending time cannot be smaller than starting time");
                    txtStartTime.Text = "";
                    txtEndTime.Text = "";
                }                                  
                else
                {
                    if (appointment.Id == 0)
                        db.Appointments.Add(appointment);
                    else
                        db.Entry(appointment).State = EntityState.Modified;
                    Clear();
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Appointment submitted successfuly");
                        PopulateDataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please enter information in the correct format");
                    }
                }
            };
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Appointment appointment1 = (Appointment)dataGridAppointment.SelectedItem;
            appointment.Id = Convert.ToInt32(appointment1.Id);
            using (HospitalDbContext db = new HospitalDbContext())
            {
                db.Appointments.Attach(appointment);
                txtPatientId.Text = appointment.PatientID.ToString();
                txtDate.Text = appointment.Date.ToString();
                txtType.Text = appointment.Type;
                txtStartTime.Text = appointment.Start.ToString();
                txtEndTime.Text = appointment.End.ToString();
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

        private void txtStartTime_GotFocus(object sender, RoutedEventArgs e)
        {
            txtStartTime.Text = "";
        }
    }
}