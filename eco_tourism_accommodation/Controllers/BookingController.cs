using eco_tourism_accommodation.Modules;
using eco_tourism_accommodation.Services;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_accommodation.Controllers;

[ApiController]
[Route("api/accommodation/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("book")]
    public async Task<IActionResult> Post(RoomBooking roomBooking)
    {
        await _bookingService.BookRoomAsync(roomBooking);
        return CreatedAtAction(nameof(Get), new { id = roomBooking.Id }, roomBooking);
    }

    [HttpGet("book/{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var booked = await _bookingService.GetBookedInfo(id);
        if (booked == null)
        {
            return NotFound();
        }

        return Ok(booked);
    }

    [HttpDelete("book/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookingService.CancelBooking(id);
        return NoContent();
    }
}