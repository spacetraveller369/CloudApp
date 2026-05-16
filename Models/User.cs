using System.ComponentModel.DataAnnotations;

namespace CloudApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Status { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
