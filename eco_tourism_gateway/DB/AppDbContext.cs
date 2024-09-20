using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eco_tourism_gateway.DB
{
    public class EventLog
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string? CaseId { get; set; }
            public required string TaskId { get; set; }
            public required DateTime StartTimestamp { get; set; }
            public required DateTime EndTimestamp { get; set; }
            public string? Resource { get; set; }
            public string? Role { get; set; }
        }

    public class EcoEventLogContext : DbContext
        {
            public DbSet<EventLog> EventLogs { get; set; }

            public EcoEventLogContext(DbContextOptions<EcoEventLogContext> options)
                : base(options)
            {
            }            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<EventLog>().ToTable("eco_tourism_tourist_EventLog");
            }

            // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // {
            //     optionsBuilder.UseMySql("YourConnectionString", 
            //         new MySqlServerVersion(new Version(8, 0, 25)));
            // }
        }
}

