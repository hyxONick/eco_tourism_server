using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eco_tourism_weather.Models;

namespace eco_tourism_weather.DB
{
    public class EcoTourismWeatherContext : DbContext
        {
            public DbSet<WeatherInfo> WeatherInfos { get; set; }

            public EcoTourismWeatherContext(DbContextOptions<EcoTourismWeatherContext> options)
                : base(options)
            {
            }            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<WeatherInfo>().ToTable("eco_tourism_tourist_WeatherInfo");
            }

            // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // {
            //     optionsBuilder.UseMySql("YourConnectionString", 
            //         new MySqlServerVersion(new Version(8, 0, 25)));
            // }
        }
}

