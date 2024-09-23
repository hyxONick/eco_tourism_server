using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_outdoor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        // GET: ActivityController/All
        [HttpGet("all")]
        public IActionResult All()
        {
            var activities = new string[] { "Hiking", "Camping", "Fishing" };
            return Ok(activities);
        }
    }
}
