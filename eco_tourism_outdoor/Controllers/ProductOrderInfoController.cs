using Microsoft.AspNetCore.Mvc;
using eco_tourism_outdoor.Services;
using eco_tourism_outdoor.Models;

namespace eco_tourism_outdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductOrderInfoController : ControllerBase
    {
        private readonly IProductOrderInfoService _productOrderInfoService;

        public ProductOrderInfoController(IProductOrderInfoService productOrderInfoService) =>
            _productOrderInfoService = productOrderInfoService;

        [HttpGet("fetch")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _productOrderInfoService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("buyer/{id:int}")]
        public async Task<IActionResult> GetByBuyer(int id)
        {
            var orders = await _productOrderInfoService.GetByBuyerIdAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _productOrderInfoService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductOrderInfo newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest();
            }

            await _productOrderInfoService.CreateAsync(newOrder);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductOrderInfo updatedOrder)
        {
            if (updatedOrder == null)
            {
                return BadRequest();
            }

            var existingOrder = await _productOrderInfoService.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _productOrderInfoService.UpdateAsync(id, updatedOrder);
            return NoContent();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingOrder = await _productOrderInfoService.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _productOrderInfoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
