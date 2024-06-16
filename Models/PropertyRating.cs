namespace HataReviewService.Models;

public class PropertyRating
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Первичный ключ
    public Guid PropertyId { get; set; }
    public double AverageRating { get; set; }
}

public class PropertyRatingDto
{
    public Guid PropertyId { get; set; }
    public double AverageRating { get; set; }
}