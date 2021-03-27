using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCmd : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteHotelCmdHandler : IRequestHandler<DeleteHotelCmd>
    {
        private readonly IApplicationDbContext _context;

        public DeleteHotelCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteHotelCmd request, CancellationToken cancellationToken)
        {
            var entity = await _context.Hotels.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Hotel), request.Id);
            }

            _context.Hotels.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
