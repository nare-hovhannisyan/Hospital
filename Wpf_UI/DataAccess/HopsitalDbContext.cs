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
    }
}
