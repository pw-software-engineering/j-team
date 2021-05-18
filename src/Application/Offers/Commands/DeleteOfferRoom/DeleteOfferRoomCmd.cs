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
    public class DeleteOfferRoomCmd : IRequest
    {
        public int OfferId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
    }

    public class DeleteOfferRoomCmdHandler : IRequestHandler<DeleteOfferRoomCmd>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOfferRoomCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOfferRoomCmd request, CancellationToken cancellationToken)
        {
            var entity = _context.Offers
                .Include(o => o.Rooms)
                .FirstOrDefault(r => r.OfferId == request.OfferId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Offer), request.OfferId);
            }
            if (entity.HotelId != request.HotelId)
                throw new ForbiddenAccessException();

            if (entity.Rooms == null)
            {
                throw new NotFoundException(nameof(Room), request.RoomId);
            }
            else
            {
                var room = entity.Rooms.Find(room => room.RoomId == request.RoomId);
                entity.Rooms.Remove(room);
                _context.Offers.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
