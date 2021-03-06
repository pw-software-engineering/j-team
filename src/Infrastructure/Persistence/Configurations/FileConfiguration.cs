﻿using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(t => t.FileId);
            builder.Property(t => t.Name)
                .HasMaxLength(200);
            builder.Property(t => t.Data)
                .IsRequired()
                .HasMaxLength(20000);
            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.HasOne(t => t.Hotel).WithMany(t => t.Pictures);
            builder.HasOne(t => t.Offer).WithMany(t => t.Pictures);
        }
    }
}