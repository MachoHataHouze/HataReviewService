using HataReviewService.Models;
using HataReviewService.Repositories;

namespace HataReviewService.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review>> GetReviewsByPropertyIdAsync(Guid propertyId)
    {
        return await _reviewRepository.GetReviewsByPropertyIdAsync(propertyId);
    }

    public async Task AddReviewAsync(ReviewDto reviewDto, int userId)
    {
        var review = new Review
        {
            PropertyId = reviewDto.PropertyId,
            UserId = userId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };

        await _reviewRepository.AddReviewAsync(review);
        await UpdatePropertyRatingAsync(reviewDto.PropertyId);
    }

    private async Task UpdatePropertyRatingAsync(Guid propertyId)
    {
        var reviews = await _reviewRepository.GetReviewsByPropertyIdAsync(propertyId);
        var averageRating = reviews.Average(r => r.Rating);

        await _reviewRepository.UpdatePropertyRatingAsync(propertyId, averageRating);
    }

    public async Task<IEnumerable<PropertyRatingDto>> GetTopRatedPropertiesAsync(int topN)
    {
        return await _reviewRepository.GetTopRatedPropertiesAsync(topN);
    }
}