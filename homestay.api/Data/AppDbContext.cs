using Microsoft.EntityFrameworkCore;
using homestay.api.Models;

namespace homestay.api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

     
        public DbSet<User> Users { get; set; }

        public DbSet<Homestay> Homestays { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Location> locations { get; set; }
    }
}