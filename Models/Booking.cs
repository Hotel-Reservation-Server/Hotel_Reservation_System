﻿using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public decimal TotalPayment { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
    }
}

 