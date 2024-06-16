namespace HataReviewService.Models;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PropertyId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}


public class ReviewDto
{
    public Guid PropertyId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}