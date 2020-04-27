using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Wpf_UI.DataAccess;
using Wpf_UI.Models;

namespace Wpf_UI
{
    public partial class PatientsWindow : Window
    {
        public PatientsWindow()
        {
            InitializeComponent();
        }

        Patient patient = new Patient();
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtName.Text = txtGender.Text = txtDateofBirth.Text = txtAddress.Text = txtPhoneNumber.Text = "";
            btnSave.Content = "Save";
            btnDelete.IsEnabled = false;
            patient.Id = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateDataGrid();
        }

        private bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[+]*[0-9][0-9]*").Success;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPhoneNumber(txtPhoneNumber.Text.Trim()))
            {
                MessageBox.Show("Please enter valid phone number");
                txtPhoneNumber.Text = "";
            }
            else
            {
                patient.Name = txtName.Text.Trim();
                patient.Gender = txtGender.Text.Trim();
                patient.DateofBirth = txtDateofBirth.DisplayDate.Date;
                patient.Address = txtAddress.Text.Trim();
                patient.TelephoneNumber = txtPhoneNumber.Text.Trim();

                using (HospitalDbContext db = new HospitalDbContext())
                {
                    if (patient.Id == 0)//insert
                        db.Patients.Add(patient);
                    else //update
                        db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                }
                Clear();
                PopulateDataGrid();
                MessageBox.Show("Patient submitted successfully");
            }
        }

        private void PopulateDataGrid()
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                datagridPatient.ItemsSource = db.Patients.ToList<Patient>();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Patient patient1 = (Patient)datagridPatient.SelectedItem;
            patient.Id = Convert.ToInt32(patient1.Id);
            using (HospitalDbContext db = new HospitalDbContext())
            {
                patient = db.Patients.Where(x => x.Id == patient.Id).FirstOrDefault();
                txtName.Text = patient.Name;
                txtGender.Text = patient.Gender;
                txtDateofBirth.Text = patient.DateofBirth.ToString();
                txtAddress.Text = patient.Address;
                txtPhoneNumber.Text = patient.TelephoneNumber;
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
                    var entry = db.Entry(patient);
                    if (entry.State == EntityState.Detached)
                        db.Patients.Attach(patient);
                    db.Patients.Remove(patient);
                    db.SaveChanges();
                    PopulateDataGrid();
                    Clear();
                    MessageBox.Show("The item was deleted successfully!");
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
