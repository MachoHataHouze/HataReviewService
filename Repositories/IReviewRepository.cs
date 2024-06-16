using HataReviewService.Models;

namespace HataReviewService.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetReviewsByPropertyIdAsync(Guid propertyId);
    Task AddReviewAsync(Review review);
    Task UpdatePropertyRatingAsync(Guid propertyId, double averageRating);
    Task<IEnumerable<PropertyRatingDto>> GetTopRatedPropertiesAsync(int topN);

}