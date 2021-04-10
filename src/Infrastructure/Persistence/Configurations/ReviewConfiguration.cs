using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(t => t.ReviewId);
            builder.Property(t => t.Content)
                .HasMaxLength(2000);
            builder.Property(t => t.Rating)
                .IsRequired();
            builder.HasOne(t => t.Client).WithMany(t => t.Reviews);
            builder.HasOne(t => t.Offer).WithMany(t => t.Reviews);
        }
    }
}