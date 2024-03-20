using Microsoft.EntityFrameworkCore;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Web.Data.Contracts
{
    public interface IMotoBalkansDbContext
    {
        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }
    }
}
