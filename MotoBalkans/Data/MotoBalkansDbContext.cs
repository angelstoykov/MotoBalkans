﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Data
{
    public class MotoBalkansDbContext : IdentityDbContext
    {
        public MotoBalkansDbContext(DbContextOptions<MotoBalkansDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rental>()
                .HasKey(k => new { k.MotorcycleId, k.CustomerId });

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
                });

            builder
                .Entity<Engine>()
                .HasData(
                new Engine()
                {
                    Id = 1,
                    EngineType = Web.Data.Enums.EngineType.Conbustion
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
                    TransmissionId = 1
                },
                new Motorcycle()
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "F800GS Adventure",
                    EngineId = 1,
                    TransmissionId = 1
                },
                new Motorcycle()
                {
                    Id = 3,
                    Brand = "Yamaha",
                    Model = "Tenere 700",
                    EngineId = 1,
                    TransmissionId = 1
                });

            base.OnModelCreating(builder);
        }

        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }
    }
}
