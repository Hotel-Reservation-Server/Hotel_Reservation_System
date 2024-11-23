using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Reservation_System.Models
{
    [Table("User")]
    public class User
    {
        [Key] 
        public int UserId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public long NIN { get; set; }
        public string? Gender { get; set; }
        public long PhoneNo { get; set; }
        public string? StateofResidence { get; set; }
    }
}
