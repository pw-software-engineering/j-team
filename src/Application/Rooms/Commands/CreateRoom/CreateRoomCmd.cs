using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelReservationSystem.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelReservationSystem.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCmd : IRequest<int>
    {
        public string HotelRoomNumber { get; set; }
        public int? OfferID { get; set; }
        public int HotelID { get; set; } = 1;
    }

    public class CreateRoomCmdHandler : IRequestHandler<CreateRoomCmd, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRoomCmd request, CancellationToken cancellationToken)
        {
            //todo: wyrzucic jak bedzie token z hotelid
            if (request.HotelID == 1)
            {
                request.HotelID = _context.Hotels.First().HotelId;
            }
            var entity = new Room
            {
                HotelRoomNumber = request.HotelRoomNumber,
                HotelId = request.HotelID,
            };

            _context.Rooms.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            if (request.OfferID.HasValue)
            {
                var offer = _context.Offers.Include(x => x.Rooms)
                .FirstOrDefault(x => x.OfferId == request.OfferID);
                offer.Rooms.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return entity.RoomId;
        }
    }
}
