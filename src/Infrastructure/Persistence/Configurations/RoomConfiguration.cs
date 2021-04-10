using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(t => t.RoomId);
            builder.Property(t => t.HotelRoomNumber)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasMany(t => t.Offers).WithMany(t => t.Rooms);
        }
    }
}