using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            var entity = new Room
            {
                HotelRoomNumber = request.HotelRoomNumber,
                OfferId = request.OfferId
            };

            _context.Rooms.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.RoomId;
        }
    }
}
