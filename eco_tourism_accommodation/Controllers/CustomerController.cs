using eco_tourism_accommodation.Services;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_accommodation.Controllers;

[ApiController]
[Route("api/accommodation/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("feedback")]
    public IActionResult Feedback()
    {
        return Ok("feedback");
    }
}