using Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Data
{
    public class BookingDbContext:DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Booking> Bookings
        {
            get;
            set;
        }
        public DbSet<Room> Rooms
        {
            get;
            set;
        }
    }
}
