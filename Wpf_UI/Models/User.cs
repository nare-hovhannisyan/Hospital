using System.ComponentModel.DataAnnotations.Schema;

namespace Wpf_UI.Models
{
    [Table("Users")]
    public class User
    { 
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
