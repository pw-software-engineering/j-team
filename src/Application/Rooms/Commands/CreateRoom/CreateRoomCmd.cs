using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelReservationSystem.Application.Common.Exceptions;

namespace HotelReservationSystem.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCmd : IRequest<int>
    {
        public string HotelRoomNumber { get; set; }
        public int OfferId { get; set; }
    }

    public class CreateOfferCmdHandler : IRequestHandler<CreateRoomCmd, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOfferCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRoomCmd request, CancellationToken cancellationToken)
        {
            var offer = await _context.Offers.FindAsync(request.OfferId);

            if (offer == null)
            {
                throw new NotFoundException(nameof(Offer), request.OfferId);
            }
            var entity = new Room
            {
                HotelRoomNumber = request.HotelRoomNumber,
                OfferId = request.OfferId,
                Offer = offer
            };

            _context.Rooms.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.RoomId;
        }
    }
}
