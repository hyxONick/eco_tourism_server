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
            modelBuilder.Entity<SceneryInfo>().ToTable("eco_tourism_tourist_SceneryInfo").HasData(
                new SceneryInfo()
                {
                    Id = 1,
                    Name = "River Boat Cruise",
                    Address = "address",
                    Price = 10.5,
                    PicUrl =
                        "https://images.unsplash.com/photo-1707007694363-b8afb46ed639?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    IsDeleted = false 
                }
            );
            modelBuilder.Entity<TouristInfo>().ToTable("eco_tourism_tourist_TouristInfo");
            modelBuilder.Entity<TouristOrderInfo>().ToTable("eco_tourism_tourist_TouristOrderInfo");

            base.OnModelCreating(modelBuilder);
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySql("YourConnectionString", 
        //         new MySqlServerVersion(new Version(8, 0, 25)));
        // }
    }
}