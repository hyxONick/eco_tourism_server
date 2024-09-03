using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eco_tourism_tourist.Models;

namespace eco_tourism_tourist.DB
{
    public class EcoTourismTouristContext : DbContext
        {
            public DbSet<SceneryInfo> SceneryInfos { get; set; }
            public DbSet<TouristInfo> TouristInfos { get; set; }
            public DbSet<TouristOrderInfo> TouristOrderInfos { get; set; }

            public EcoTourismTouristContext(DbContextOptions<EcoTourismTouristContext> options)
                : base(options)
            {
            }            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<SceneryInfo>().ToTable("eco_tourism_tourist_SceneryInfo");
                modelBuilder.Entity<TouristInfo>().ToTable("eco_tourism_tourist_TouristInfo");
                modelBuilder.Entity<TouristOrderInfo>().ToTable("eco_tourism_tourist_TouristOrderInfo");
            }

            // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // {
            //     optionsBuilder.UseMySql("YourConnectionString", 
            //         new MySqlServerVersion(new Version(8, 0, 25)));
            // }
        }
}

