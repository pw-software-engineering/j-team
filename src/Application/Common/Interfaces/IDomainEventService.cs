using HotelReservationSystem.Domain.Common;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
