using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Commands.DeleteOffer
{
    public class AddOfferRoomCmd : IRequest
    {
        public int OfferId { get; set; }
        public int RoomId { get; set; }
    }

    public class AddOfferRoomCmdHandler : IRequestHandler<AddOfferRoomCmd>
    {
        private readonly IApplicationDbContext _context;

        public AddOfferRoomCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddOfferRoomCmd request, CancellationToken cancellationToken)
        {
            var entity = _context.Offers
                .Include(o => o.Rooms)
                .FirstOrDefault(r => r.OfferId == request.OfferId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Offer), request.OfferId);
            }
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == request.RoomId);
            if (room == null)
            {
                throw new NotFoundException(nameof(Room), request.RoomId);
            }
            else
            {
                entity.Rooms.Add(room);
                _context.Offers.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
