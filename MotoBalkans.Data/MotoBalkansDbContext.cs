using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data.Models;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Data
{
    public class MotoBalkansDbContext : IdentityDbContext, IMotoBalkansDbContext
    {
        public MotoBalkansDbContext(DbContextOptions<MotoBalkansDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rental>()
                .HasOne(m => m.Motorcycle)
                .WithMany(m => m.Customers);

            builder
                .Entity<Transmission>()
                .HasData(
                new Transmission()
                {
                    Id = 1,
                    TransmissionType = Web.Data.Enums.TransmissionType.Manual
                },
                new Transmission()
                {
                    Id = 2,
                    TransmissionType = Web.Data.Enums.TransmissionType.Automatic
                }
                );

            builder
                .Entity<Engine>()
                .HasData(
                new Engine()
                {
                    Id = 1,
                    EngineType = Web.Data.Enums.EngineType.Conbustion
                },
                new Engine()
                {
                    Id = 2,
                    EngineType = Web.Data.Enums.EngineType.Electric
                },
                new Engine()
                {
                    Id = 3,
                    EngineType = Web.Data.Enums.EngineType.Other
                });

            builder
                .Entity<Motorcycle>()
                .HasData(
                new Motorcycle()
                {
                    Id = 1,
                    Brand = "Honda",
                    Model = "TransAlp",
                    EngineId = 1,
                    TransmissionId = 1,
                    PricePerDay = 100m
                },
                new Motorcycle()
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "F800GS Adventure",
                    EngineId = 1,
                    TransmissionId = 1,
                    PricePerDay = 150m
                },
                new Motorcycle()
                {
                    Id = 3,
                    Brand = "Yamaha",
                    Model = "Tenere 700",
                    EngineId = 1,
                    TransmissionId = 1,
                    PricePerDay = 200m
                });

            builder
                .Entity<Report>()
                .HasData(
                new Report()
                {
                    Id = 1,
                    Name = "Get all rentals"
                });

            base.OnModelCreating(builder);
        }

        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public DbSet<Report> Reports { get; set; }
    }
}
