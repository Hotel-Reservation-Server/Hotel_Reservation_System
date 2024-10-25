using Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Data
{
    public class RoomDbContext: DbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Room> Rooms
        {
            get;
            set;
        }
    }
}
