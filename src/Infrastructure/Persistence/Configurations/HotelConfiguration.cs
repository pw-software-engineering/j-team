using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(t => t.HotelId);
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.HotelPreviewPicture)
                .IsRequired(false);
            builder.Property(t => t.Pictures)
                .IsRequired(false);
            builder.Property(t => t.Description)
                .HasMaxLength(2000)
                .IsRequired(false);
            builder.Property(t => t.City)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Country)
                .HasMaxLength(200)
                .IsRequired();
            builder.HasMany(t => t.Offers).WithOne(t => t.Hotel);
        }
    }
}