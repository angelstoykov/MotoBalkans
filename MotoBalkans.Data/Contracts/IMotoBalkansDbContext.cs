using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data.Models;

namespace MotoBalkans.Data.Contracts
{
    public interface IMotoBalkansDbContext
    {
        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }
    }
}
