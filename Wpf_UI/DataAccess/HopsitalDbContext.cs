namespace Wpf_UI.DataAccess
{
    using System;
    using System.Data.Entity;
    using Wpf_UI.Models;

    public class HospitalDbContext : DbContext
    {


        public HospitalDbContext()
            : base("name=HospitalDbContext")
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          /*  modelBuilder.Entity<Patient>()(
                new Patient
                {
                    Name = "John",
                    Gender = "Male",
                    DateofBirth = new DateTime(1995, 05, 04),
                    Address = "Address",
                    TelephoneNumber = "098888777"
                }
                );*/
        }

        /*        Patient patient = new Patient();
                patient.Name = "Ann";
                        patient.Gender = "Female";
                        patient.DateofBirth = new DateTime(1992, 06, 10);
                patient.Address = "Address 1";
                        patient.TelephoneNumber = "+112554987";

                        Appointment appointment = new Appointment();
                appointment.PatientID = 1;
                        appointment.Date = new DateTime(2020, 04, 30, 09, 00, 0);
                appointment.Type = "Primary";
                        appointment.Diagnosis = "Diagnosis 1";

                        User user = new User();
                user.Login = "admin";
                        user.Password = "admin";

                        db.Patients.Add(patient);
                        db.Appointments.Add(appointment);
                        db.Users.Add(user);
                        db.SaveChanges();*/
    }
}
