using Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<User> Users
        {
            get;
            set;
        }
        public DbSet<Admin> Admins
        {
            get;
            set;
        }
    }
}
