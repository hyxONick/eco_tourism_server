using eco_tourism_user.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_user.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        // GET: UserController/Get/5
        [HttpGet("user")]
        public IActionResult Get([FromQuery]int id)
        {
            return Ok(new
            {
                Message = $"Hello from user detail endpoint: user id is {id}"
            });
        }
        // POST: UserController/Create
        [HttpPost("create")]
        public IActionResult Create([FromBody] User user)
        {
            return Ok(new
            {
                Message = $"Hello from create user endpoint: user name is {user.Name}"
            });
        }
    }
}
