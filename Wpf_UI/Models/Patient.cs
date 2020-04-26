using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wpf_UI.Models
{
    [Table("Patients")]
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        [Column(TypeName ="date")]
        public DateTime DateofBirth { get; set; } = DateTime.MinValue;
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
