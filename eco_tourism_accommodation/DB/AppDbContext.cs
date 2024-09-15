using eco_tourism_accommodation.Modules;
using Microsoft.EntityFrameworkCore;

namespace eco_tourism_accommodation.DB;

public class EcoTourismAccommodationContext : DbContext
{
    public DbSet<RoomInfo> RoomInfos { get; set; }
    public DbSet<RoomBooking> RoomBookings { get; set; }

    public EcoTourismAccommodationContext(DbContextOptions<EcoTourismAccommodationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoomInfo>().ToTable("eco_tourism_accommodation_RoomInfo");
        modelBuilder.Entity<RoomBooking>().ToTable("eco_tourism_accommodation_RoomBooking");
    }
}