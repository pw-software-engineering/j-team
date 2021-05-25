using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(t => t.OfferId);
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Description)
                .HasMaxLength(200);
            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);
            builder.Property(t => t.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(t => t.CostPerChild)
                .IsRequired()
                .HasPrecision(2);
            builder.Property(t => t.CostPerAdult)
                .IsRequired()
                .HasPrecision(2);
            builder.Property(t => t.MaxGuests)
                .IsRequired();
            builder.HasOne(t => t.Hotel).WithMany(t => t.Offers);
            builder.HasMany(t => t.Rooms).WithMany(t => t.Offers);
            builder.HasMany(t => t.Reservations).WithOne(t => t.Offer);
            builder.HasOne(t => t.OfferPreviewPicture).WithOne().HasForeignKey<Offer>(x => x.OfferPreviewPictureId);
            builder.HasMany(t => t.Pictures).WithOne(t => t.Offer);
            builder.HasMany(t => t.Reviews).WithOne(t => t.Offer);
        }
    }
}