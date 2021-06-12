using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservationSystem.Infrastructure.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(t => t.ClientId);
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Surname)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Username)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Email)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Password)
                .HasMaxLength(20)
                .IsRequired();
            builder.HasMany(t => t.Reservations).WithOne(t => t.Client);
            builder.HasMany(t => t.Reviews).WithOne(t => t.Client);
        }
    }
}