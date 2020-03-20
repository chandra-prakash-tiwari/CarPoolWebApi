using CarPoolingWebApi.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingWebApi.Context
{
    public class CarPoolingContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Ride> Rides { get; set; }

        public DbSet<Car> Cars { get; set; }

        public CarPoolingContext(DbContextOptions options) : base(options)
        {
        }
    }
}
