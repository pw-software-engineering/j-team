using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Hotel> Hotels { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<OfferFile> OfferFiles { get; set; }
        DbSet<PreviewOfferFile> PreviewOfferFiles { get; set; }
        DbSet<HotelFile> HotelFiles { get; set; }
        DbSet<PreviewHotelFile> PreviewHotelFiles { get; set; }
        DbSet<Review> Reviews { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
