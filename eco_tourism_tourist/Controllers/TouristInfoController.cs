using Microsoft.AspNetCore.Mvc;
using eco_tourism_tourist.Models;
using eco_tourism_tourist.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eco_tourism_tourist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TouristInfoController : ControllerBase
    {
        private readonly ITouristInfoService _touristInfoService;

        public TouristInfoController(ITouristInfoService touristInfoService)
        {
            _touristInfoService = touristInfoService;
        }

        [HttpGet("fetch")]
        public async Task<ActionResult<List<TouristInfo>>> Get()
        {
            return Ok(await _touristInfoService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TouristInfo>> Get(int id)
        {
            var touristInfo = await _touristInfoService.GetByIdAsync(id);

            if (touristInfo == null)
            {
                return NotFound();
            }

            return Ok(touristInfo);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(TouristInfo newTouristInfo)
        {
            if (newTouristInfo == null)
            {
                return BadRequest();
            }

            await _touristInfoService.CreateAsync(newTouristInfo);
            return CreatedAtAction(nameof(Get), new { id = newTouristInfo.Id }, newTouristInfo);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, TouristInfo updatedTouristInfo)
        {

            if (updatedTouristInfo == null)
            {
                return BadRequest();
            }

            var existingTouristInfo = await _touristInfoService.GetByIdAsync(id);

            if (existingTouristInfo == null)
            {
                return NotFound();
            }

            await _touristInfoService.UpdateAsync(id, updatedTouristInfo);

            return NoContent();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTouristInfo = await _touristInfoService.GetByIdAsync(id);

            if (existingTouristInfo == null)
            {
                return NotFound();
            }

            await _touristInfoService.DeleteAsync(id);

            return NoContent();
        }

        // [HttpGet("search")]
        // public async Task<ActionResult<List<TouristInfo>>> Search([FromQuery] string searchTerm)
        // {
        //     var results = await _touristInfoService.SearchAsync(searchTerm);
        //     return Ok(results);
        // }
    }
}
