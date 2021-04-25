using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(t => t.ReservationId);
            builder.Property(t => t.FromTime)
                .IsRequired();
            builder.Property(t => t.FromTime)
                .IsRequired();
            builder.Property(t => t.ChildrenCount)
                .IsRequired();
            builder.Property(t => t.AdultsCount)
                .IsRequired();
            builder.HasOne(t => t.Client).WithMany(t => t.Reservations);
            builder.HasOne(t => t.Offer).WithMany(t => t.Reservations);
            builder.HasOne(t => t.Room).WithMany(t => t.Reservations);
        }
    }
}