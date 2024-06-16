using HataReviewService.Models;

namespace HataReviewService.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetReviewsByPropertyIdAsync(Guid propertyId);
    Task AddReviewAsync(ReviewDto reviewDto, int userId);
    Task<IEnumerable<PropertyRatingDto>> GetTopRatedPropertiesAsync(int topN);
}