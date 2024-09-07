using Microsoft.AspNetCore.Mvc;
using eco_tourism_tourist.Models;
using eco_tourism_tourist.Services;

namespace eco_tourism_tourist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TouristOrderInfoController : ControllerBase
    {
        private readonly ITouristOrderInfoService _touristOrderInfoService;

        public TouristOrderInfoController(ITouristOrderInfoService touristOrderInfoService) =>
            _touristOrderInfoService = touristOrderInfoService;

        [HttpGet("fetch")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _touristOrderInfoService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _touristOrderInfoService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TouristOrderInfo newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest();
            }

            await _touristOrderInfoService.CreateAsync(newOrder);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TouristOrderInfo updatedOrder)
        {
            if (updatedOrder == null)
            {
                return BadRequest();
            }

            var existingOrder = await _touristOrderInfoService.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _touristOrderInfoService.UpdateAsync(id, updatedOrder);
            return NoContent();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingOrder = await _touristOrderInfoService.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _touristOrderInfoService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            var results = await _touristOrderInfoService.SearchAsync(searchTerm);
            return Ok(results);
        }
    }
}
