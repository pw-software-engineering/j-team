using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class PreviewFileConfiguration : IEntityTypeConfiguration<PreviewFile>
    {
        public void Configure(EntityTypeBuilder<PreviewFile> builder)
        {
            builder.HasKey(t => t.FileId);
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(t => t.Data)
                .IsRequired()
                .HasMaxLength(20000);
            builder.Property(t => t.Description)
                .HasMaxLength(2000)
                .IsRequired();
            builder.HasOne(t => t.Hotel).WithOne(t => t.HotelPreviewPicture);
            builder.HasMany(t => t.Offers).WithOne(t => t.OfferPreviewPicture);
        }
    }
}