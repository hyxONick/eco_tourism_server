using Microsoft.AspNetCore.Mvc;
using eco_tourism_outdoor.Services;
using eco_tourism_outdoor.Models;

namespace eco_tourism_outdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentRentalController : ControllerBase
    {
        private readonly IEquipmentRentalService _equipmentRentalService;

        public EquipmentRentalController(IEquipmentRentalService equipmentRentalService) =>
            _equipmentRentalService = equipmentRentalService;

        [HttpGet("fetch")]
        public async Task<IActionResult> GetAll()
        {
            var rentals = await _equipmentRentalService.GetAllAsync();
            return Ok(rentals);
        }

        [HttpGet("renter/{id:int}")]
        public async Task<IActionResult> GetByRenter(int id)
        {
            var rental = await _equipmentRentalService.GetByRenterIdAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var rental = await _equipmentRentalService.GetByIdAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] EquipmentRental newRental)
        {
            if (newRental == null)
            {
                return BadRequest();
            }

            await _equipmentRentalService.CreateAsync(newRental);
            return CreatedAtAction(nameof(Get), new { id = newRental.Id }, newRental);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipmentRental updatedRental)
        {
            if (updatedRental == null)
            {
                return BadRequest();
            }

            var existingRental = await _equipmentRentalService.GetByIdAsync(id);
            if (existingRental == null)
            {
                return NotFound();
            }

            await _equipmentRentalService.UpdateAsync(id, updatedRental);
            return NoContent();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRental = await _equipmentRentalService.GetByIdAsync(id);
            if (existingRental == null)
            {
                return NotFound();
            }

            await _equipmentRentalService.DeleteAsync(id);
            return NoContent();
        }
    }
}
