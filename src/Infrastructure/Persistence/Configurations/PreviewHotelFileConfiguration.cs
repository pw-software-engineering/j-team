using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class PreviewHotelFileConfiguration : IEntityTypeConfiguration<PreviewHotelFile>
    {
        public void Configure(EntityTypeBuilder<PreviewHotelFile> builder)
        {
            builder.HasKey(t => t.FileId);
            builder.Property(t => t.Name)
                .HasMaxLength(200);
            builder.Property(t => t.Data)
                .IsRequired()
                .HasMaxLength(20000);
            builder.Property(t => t.Description)
                .HasMaxLength(2000);
            builder.HasOne(t => t.Hotel).WithOne(t => t.HotelPreviewPicture);
        }
    }
}