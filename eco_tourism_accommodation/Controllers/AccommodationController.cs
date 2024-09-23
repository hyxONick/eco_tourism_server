using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_accommodation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccommodationController : ControllerBase
    {
        // GET: AccommodationController/All
        [HttpGet("hotels")]
        public IActionResult All()
        {
            var hotels = new string[] { "The Start", "The Crown" };
            return Ok(hotels);
        }
    }
}
