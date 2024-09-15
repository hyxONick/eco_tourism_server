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

    [HttpPost("book-room")]
    public IActionResult BookRoom(RoomInfo roomInfo)
    {
        return Ok("Book room");
    }
}