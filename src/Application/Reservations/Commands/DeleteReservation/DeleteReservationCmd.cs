using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using FluentValidation.Results;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCmd : IRequest<int>
    {
        public int ReservationId;
        public int ClientId;
    }

    public class DeleteReservationCmdHandler : IRequestHandler<DeleteReservationCmd, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteReservationCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteReservationCmd request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(request.ReservationId);
            if (reservation == null)
                throw new NotFoundException("Reservation with ID equal to reservationID path parameter doesn't exist");
            if (reservation.ClientId != request.ClientId)
                throw new ForbiddenAccessException();
            if (DateTime.Compare(reservation.FromTime, DateTime.Now) < 0)
                throw new ValidationException(new ValidationFailure[]
                {
                    new ValidationFailure(nameof(Reservation), "Reservation is currently underway or already completed")
                });


            _context.Reservations.Remove(reservation);
            var intersectedReservations = _context.Reservations.Where(r =>
                    r.OfferId == reservation.OfferId &&
                    DateTime.Compare(r.FromTime, DateTime.Now) > 0 &&
                    DateTime.Compare(r.FromTime, reservation.ToTime) < 0 &&
                    DateTime.Compare(r.ToTime, reservation.FromTime) > 0)
                .OrderByDescending(r => r.RoomId)
                .ToList();

            await _context.SaveChangesAsync(cancellationToken);
            foreach (var intersectedReservation in intersectedReservations)
            {
                var availableRooms = _context.Rooms
                    .Where(room => (room.Offers.Where(o => o.OfferId == intersectedReservation.OfferId).Any()
                                    && !room.Reservations.Where(r =>
                                        (r.ToTime.CompareTo(intersectedReservation.FromTime) >= 0
                                         && r.FromTime.CompareTo(intersectedReservation.ToTime) <= 0)).Any()))
                    .ToList();
                if (availableRooms.Any() && intersectedReservation.RoomId.HasValue)
                {
                    intersectedReservation.RoomId = Math.Min(intersectedReservation.RoomId.Value,
                        availableRooms.Min(r => r.RoomId));
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return reservation.ReservationId;
        }
    }
}