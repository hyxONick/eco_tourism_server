using eco_tourism_accommodation.DB;
using eco_tourism_accommodation.Modules;
using Microsoft.EntityFrameworkCore;

namespace eco_tourism_accommodation.Services;

public interface IBookingService
{
    Task BookRoomAsync(RoomBooking roomBooking);

    Task CancelBooking(int id);

    Task<RoomBooking?> GetBookedInfo(int gustId);
}

public class BookingService : IBookingService
{
    private readonly EcoTourismAccommodationContext context;

    public BookingService(EcoTourismAccommodationContext context)
    {
        this.context = context;
    }

    public async Task BookRoomAsync(RoomBooking roomBooking)
    {
        context.RoomBookings.Add(roomBooking);
        await context.SaveChangesAsync();
    }

    public async Task CancelBooking(int id)
    {
        RoomBooking? booking = await context.RoomBookings
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

        if (booking != null)
        {
            booking.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }

    public async Task<RoomBooking?> GetBookedInfo(int id)
    {
        return await context.RoomBookings.FirstOrDefaultAsync(r => !r.IsDeleted && r.Id == id);
    }
}