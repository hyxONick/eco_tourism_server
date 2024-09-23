using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eco_tourism_outdoor.Models;

namespace eco_tourism_outdoor.DB
{
    public class EcoTourismOutdoorContext : DbContext
        {
            public DbSet<EquipmentRental> EquipmentRental { get; set; }
            public DbSet<ProductInfo> ProductInfo { get; set; }
            public DbSet<ProductOrderInfo> ProductOrderInfo { get; set; }

            public EcoTourismOutdoorContext(DbContextOptions<EcoTourismOutdoorContext> options)
                : base(options)
            {
            }            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<EquipmentRental>().ToTable("eco_tourism_outdoor_EquipmentRental");
                modelBuilder.Entity<ProductInfo>().ToTable("eco_tourism_outdoor_ProductInfo");
                modelBuilder.Entity<ProductOrderInfo>().ToTable("eco_tourism_outdoor_ProductOrderInfo");
            }

            // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // {
            //     optionsBuilder.UseMySql("YourConnectionString", 
            //         new MySqlServerVersion(new Version(8, 0, 25)));
            // }
        }
}

