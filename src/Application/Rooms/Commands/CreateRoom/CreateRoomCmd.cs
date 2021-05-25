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

        public int HotelID { get; set; }
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

            var rooms = _context.Rooms.Where(x => x.HotelId == request.HotelID);

            foreach (var room_ in rooms)
            {
                if (room_.HotelRoomNumber == request.HotelRoomNumber)
                {
                    throw new System.InvalidOperationException("Room with given number already exists");
                }
            }

            var entity = new Room
            {
                HotelRoomNumber = request.HotelRoomNumber,
                HotelId = request.HotelID,
            };

            _context.Rooms.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);


            return entity.RoomId;
        }
    }
}
