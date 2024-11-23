using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservation_System.Data;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.Dto;
using System.Security.Claims;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingDbContext _context;

        public BookingsController(BookingDbContext context)
        {
            _context = context;
        }
        

        
        // GET: api/Bookings
        [HttpGet]
        public IQueryable<BookingDto> Getuser()
        {
            var getbooking = from booking in _context.Bookings
                          select new BookingDto()
                          {
                              Id = booking.Id,
                              UserId = booking.UserId,
                              RoomId = booking.RoomId,
                              CheckInDate = booking.CheckInDate,
                              CheckOutDate = booking.CheckOutDate
                          };

            return getbooking;
        }


        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
          if (_context.Bookings == null)
          {
              return NotFound();
          }
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        
        [HttpPost]
        public IActionResult CreateBooking(BookingDto bookingDto)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'BookingDbContext.Bookings' is null.");
            }
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == bookingDto.RoomId);
            if (room == null)
                return BadRequest("Invalid Room ID.");
            var nights = (bookingDto.CheckOutDate - bookingDto.CheckInDate).Days;
            if (nights <= 0)
                return BadRequest("Check-out date must be after check-in date.");

            // Calculate total payment
            var totalPayment = room.Price * nights;

            var booking = new Booking
            {
                UserId = bookingDto.UserId,
                RoomId = bookingDto.RoomId,
                TotalPayment = totalPayment,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
            };
            return Ok(new
            {
                Message = "Booking created successfully",
                Id = booking.Id,
                TotalPayment = booking.TotalPayment
            });
        }
        [HttpDelete]
        public IActionResult DeleteUser(int Id)
        {
            var booking = _context.Bookings.Find(Id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return Ok();
        }

    }
}
