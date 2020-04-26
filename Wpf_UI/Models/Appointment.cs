using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wpf_UI.Models
{
    [Table("Appointments")]
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        [Column(TypeName ="date")]
        public DateTime Date { get; set; } = DateTime.MinValue;
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Type { get; set; }
        public string Diagnosis { get; set; }
        public Patient Patient { get; set; }
    }
}
