using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.Dto
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long NIN { get; set; }
        public string? Gender { get; set; }
        public long PhoneNo { get; set; }
        public string? StateofResidence { get; set; }
    }
}
