using eco_tourism_accommodation.Services;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_accommodation.Controllers;

[ApiController]
[Route("api/accommodation/[controller]")]
public class RewardController : ControllerBase
{
    private readonly IRewardService _rewardService;

    public RewardController(IRewardService rewardService)
    {
        _rewardService = rewardService;
    }

    [HttpGet("member-rewards")]
    public IActionResult MemberRewards()
    {
        return Ok("member rewards");
    }
}