﻿using Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Data
{
    public class AdminDbContext:DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Admin> Admins
        {
            get;
            set;
        }
    }
}
