using System;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using HotelReservationSystem.Application.Common.Exceptions;

namespace HotelReservationSystem.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCmd : IRequest<int>
    {
        public int? ClientId;
        public int HotelId;
        public int OfferId;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfAdults { get; set; }
    }

    public class CreateReservationCmdHandler : IRequestHandler<CreateReservationCmd, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateReservationCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateReservationCmd request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FindAsync(request.HotelId);
            if (hotel == null)
                throw new NotFoundException(nameof(Hotel), request.HotelId);
            
            var offer = await _context.Offers.FindAsync(request.OfferId);
            if (offer == null)
                throw new NotFoundException(nameof(Offer), request.OfferId);

            List<ValidationFailure> validationFailures = new List<ValidationFailure>();
            if (DateTime.Compare(request.From, request.To) > 0)
                validationFailures.Add(new ValidationFailure(nameof(offer), 
                    "From must be no lower than To."));
            if (!offer.IsActive.Value)
                validationFailures.Add(new ValidationFailure(nameof(offer), "Offer is not active"));
            if (offer.IsDeleted.Value)
                validationFailures.Add(new ValidationFailure(nameof(offer), "Offer is deleted"));
            var availableRooms = _context.Rooms
                .Where(room => (room.Offers.Where(o => o.OfferId == request.OfferId).Any()
                                && !room.Reservations.Where(r => (r.ToTime.CompareTo(request.From) >= 0
                                                                  && r.FromTime.CompareTo(request.To) <= 0)).Any()));
            if (!availableRooms.Any())
                validationFailures.Add(new ValidationFailure(nameof(offer), 
                    "Offer is not available - please refresh information related to the offer availability"));
            
            if (validationFailures.Count > 0)
                throw new ValidationException(validationFailures);
            
            var reservation = new Reservation
            {
                OfferId = request.OfferId,
                ClientId = request.ClientId ?? 1,
                RoomId = availableRooms.Min(room => room.RoomId),
                FromTime = request.From,
                ToTime = request.To,
                ChildrenCount = request.NumberOfChildren,
                AdultsCount = request.NumberOfAdults
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);
            return reservation.ReservationId;
        }
    }
}
