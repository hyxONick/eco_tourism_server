using Microsoft.AspNetCore.Mvc;
using eco_tourism_outdoor.Services;
using eco_tourism_outdoor.Models;

namespace eco_tourism_outdoor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductInfoController : ControllerBase
    {
        private readonly IProductInfoService _productInfoService;

        public ProductInfoController(IProductInfoService productInfoService) =>
            _productInfoService = productInfoService;

        [HttpGet("fetch")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productInfoService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productInfoService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductInfo newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest();
            }

            await _productInfoService.CreateAsync(newProduct);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductInfo updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest();
            }

            var existingProduct = await _productInfoService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productInfoService.UpdateAsync(id, updatedProduct);
            return NoContent();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await _productInfoService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productInfoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
