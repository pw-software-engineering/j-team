using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCmd : IRequest
    {
        public int Id { get; set; }
        //TODO: swap for value from token
        public int HotelId { get; set; } = 1;
    }

    public class DeleteOfferCmdHandler : IRequestHandler<DeleteRoomCmd>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime dateTime;

        public DeleteOfferCmdHandler(IApplicationDbContext context, IDateTime dateTime)
        {
            this.dateTime = dateTime;
            _context = context;
        }
        private bool RangeOverlaps(DateTime t1Start, DateTime t1End, DateTime t2Start, DateTime t2End)
        {
            return t2End > t1Start && t2Start < t1End;
        }
        public async Task<Unit> Handle(DeleteRoomCmd request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rooms.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.RoomId == request.Id);

            if (entity == null || entity.HotelId != request.HotelId)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }
            var currentTime = dateTime.Now;
            var notDoneReservations = entity.Reservations.Where(x => x.ToTime > currentTime).ToList();
            var doneReservations = entity.Reservations.Where(x => x.ToTime <= currentTime).ToList();
            if (notDoneReservations.Any())
            {
                foreach (var reservation in notDoneReservations)
                {
                    var allRooms = await _context.Offers
                    .Include(x => x.Rooms)
                    .ThenInclude(x => x.Reservations)
                    .FirstOrDefaultAsync(x => x.OfferId == reservation.OfferId);
                    var freeRooms = allRooms.Rooms
                    .Where(x => !x.Reservations
                            .Any(r => RangeOverlaps(reservation.FromTime,
                                                    reservation.ToTime,
                                                    r.FromTime,
                                                    r.ToTime)));
                    if (freeRooms.Any())
                    {
                        reservation.Room = freeRooms.First();
                        reservation.RoomId = reservation.RoomId;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        throw new InvalidOperationException("Room with pending reservations can't be removed");
                    }
                }
            }
            foreach (var reservation in doneReservations)
            {
                reservation.Room = null;
                reservation.RoomId = null;
            }

            _context.Rooms.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
