using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCmd : IRequest
    {
        public int Id { get; set; }

        public string HotelRoomNumber { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateRoomCmd>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRoomCmd request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rooms.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }

            entity.HotelRoomNumber = request.HotelRoomNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
