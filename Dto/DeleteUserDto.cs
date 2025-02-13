﻿using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation_System.Dto
{
    public class DeleteUserDto
    {
        public string? FirstName { get; set; }
      
        public string? LastName { get; set; }
       
        public string? Email { get; set; }
       
        public string? Password { get; set; }
        
        public int NIN { get; set; }
        public string? Gender { get; set; }
        public int PhoneNo { get; set; }
        public string? StateofResidence { get; set; }
    }
}
