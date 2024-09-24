using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eco_tourism_user.Models;

namespace eco_tourism_user.DB
{
    public class EcoTourismUserContext : DbContext
        {
            public DbSet<User> User { get; set; }
            public EcoTourismUserContext(DbContextOptions<EcoTourismUserContext> options)
                : base(options)
            {
            }            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>().ToTable("eco_tourism_user_userInfo");
            }

            // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // {
            //     optionsBuilder.UseMySql("YourConnectionString", 
            //         new MySqlServerVersion(new Version(8, 0, 25)));
            // }
        }
}

