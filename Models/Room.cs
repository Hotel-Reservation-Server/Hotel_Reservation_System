using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; } 
        public string Type { get; set; } 
        public decimal Price { get; set; } 
        public bool IsAvailable { get; set; } 
        public string Amenities { get; set; } 
    }
}
