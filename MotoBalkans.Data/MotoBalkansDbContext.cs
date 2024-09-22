using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Data.Enums;
using MotoBalkans.Data.Models;

namespace MotoBalkans.Data
{
    public class MotoBalkansDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IMotoBalkansDbContext
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
                    TransmissionType = TransmissionType.Manual
                },
                new Transmission()
                {
                    Id = 2,
                    TransmissionType = TransmissionType.Automatic
                }
                );

            builder
                .Entity<Engine>()
                .HasData(
                new Engine()
                {
                    Id = 1,
                    EngineType = EngineType.Conbustion
                },
                new Engine()
                {
                    Id = 2,
                    EngineType = EngineType.Electric
                },
                new Engine()
                {
                    Id = 3,
                    EngineType = EngineType.Other
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
                    PricePerDay = 100m,
                    PictureUrl = "https://dizzyriders.bg/uploads/avtomobili/11_2022/421227_23YM_XL750_Transalp.jpg"
                },
                new Motorcycle()
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "F800GS Adventure",
                    EngineId = 1,
                    TransmissionId = 1,
                    PricePerDay = 150m,
                    PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRjudx8BnnMOEP6Gb2zki94D0gBoFxuwbsWc_VKS0J74A&s"
                },
                new Motorcycle()
                {
                    Id = 3,
                    Brand = "Yamaha",
                    Model = "Tenere 700",
                    EngineId = 1,
                    TransmissionId = 1,
                    PricePerDay = 200m,
                    PictureUrl = "https://advanywhere.com/wp-content/uploads/2022/12/AdvAnywhere-Tenere-review-1920x1280.jpg"
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

        public DbSet<ApplicationRole> Roles { get; set; }
    }
}
