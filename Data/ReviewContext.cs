using HataReviewService.Models;
using Microsoft.EntityFrameworkCore;

namespace HataReviewService.Data;

public class ReviewContext : DbContext
{
    public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) { }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<PropertyRating> PropertyRatings { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PropertyRating>()
            .HasKey(pr => pr.Id);
        
        modelBuilder.Entity<PropertyRating>()
            .HasIndex(pr => pr.PropertyId)
            .IsUnique();
    }
}