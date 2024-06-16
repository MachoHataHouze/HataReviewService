using System.Security.Claims;
using HataReviewService.Models;
using HataReviewService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HataReviewService.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetReviews(Guid propertyId)
    {
        var reviews = await _reviewService.GetReviewsByPropertyIdAsync(propertyId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { Message = "Unauthorized" });
        }

        await _reviewService.AddReviewAsync(reviewDto, userId);
        return NoContent();
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopRatedProperties([FromQuery] int topN = 10)
    {
        var properties = await _reviewService.GetTopRatedPropertiesAsync(topN);
        return Ok(properties);
    }
}