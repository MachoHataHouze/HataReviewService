using HataReviewService.Data;
using HataReviewService.Models;
using Microsoft.EntityFrameworkCore;

namespace HataReviewService.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ReviewContext _context;

    public ReviewRepository(ReviewContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetReviewsByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Reviews.Where(r => r.PropertyId == propertyId).ToListAsync();
    }

    public async Task AddReviewAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePropertyRatingAsync(Guid propertyId, double averageRating)
    {
        var propertyRating = await _context.PropertyRatings.FirstOrDefaultAsync(pr => pr.PropertyId == propertyId);
        if (propertyRating == null)
        {
            propertyRating = new PropertyRating
            {
                PropertyId = propertyId,
                AverageRating = averageRating
            };
            _context.PropertyRatings.Add(propertyRating);
        }
        else
        {
            propertyRating.AverageRating = averageRating;
            _context.PropertyRatings.Update(propertyRating);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PropertyRatingDto>> GetTopRatedPropertiesAsync(int topN)
    {
        return await _context.PropertyRatings
            .OrderByDescending(pr => pr.AverageRating)
            .Take(topN)
            .Select(pr => new PropertyRatingDto
            {
                PropertyId = pr.PropertyId,
                AverageRating = pr.AverageRating
            })
            .ToListAsync();
    }
}