﻿namespace Hotel_Reservation_System.Models
{
    public class Room
    {
        public int Id { get; set; } 
        public string Type { get; set; } 
        public decimal Price { get; set; } 
        public bool IsAvailable { get; set; } 
        public string Amenities { get; set; } 
    }
}
