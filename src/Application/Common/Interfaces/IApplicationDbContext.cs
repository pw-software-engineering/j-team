using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Hotel> Hotels { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
