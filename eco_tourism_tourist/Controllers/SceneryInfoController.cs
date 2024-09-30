using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using eco_tourism_tourist.Services;
using eco_tourism_tourist.Models;

namespace eco_tourism_tourist.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class SceneryInfoController : ControllerBase
    {
        private readonly ISceneryInfoService _sceneryInfoService;

        public SceneryInfoController(ISceneryInfoService sceneryInfoService) =>
            _sceneryInfoService = sceneryInfoService;

        [HttpGet("fetch")]
        public async Task<IActionResult> GetAll()
        {
            var sceneryInfos = await _sceneryInfoService.GetAllAsync();
            return Ok(sceneryInfos);
        }

        [HttpGet("getSceneryInfo/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var sceneryInfo = await _sceneryInfoService.GetByIdAsync(id);

            if (sceneryInfo == null)
            {
                return NotFound();
            }

            return Ok(sceneryInfo);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SceneryInfo newSceneryInfo)
        {
            if (newSceneryInfo == null)
            {
                return BadRequest();
            }

            await _sceneryInfoService.CreateAsync(newSceneryInfo);
            return CreatedAtAction(nameof(Get), new { id = newSceneryInfo.Id }, newSceneryInfo);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SceneryInfo updatedSceneryInfo)
        {
            if (updatedSceneryInfo == null)
            {
                return BadRequest();
            }

            var existingSceneryInfo = await _sceneryInfoService.GetByIdAsync(id);
            if (existingSceneryInfo == null)
            {
                return NotFound();
            }

            await _sceneryInfoService.UpdateAsync(id, updatedSceneryInfo);
            return NoContent();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingSceneryInfo = await _sceneryInfoService.GetByIdAsync(id);
            if (existingSceneryInfo == null)
            {
                return NotFound();
            }

            await _sceneryInfoService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            var results = await _sceneryInfoService.SearchAsync(searchTerm);
            return Ok(results);
        }
    }
}
