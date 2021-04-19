using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var entity = _context.Hotels
                .Include(x => x.HotelPreviewPicture)
                .Include(x => x.Pictures)
                .FirstOrDefault(r => r.HotelId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Hotel), request.Id);
            }
            if (entity.HotelPreviewPicture != null)
            {
                _context.Files.Remove(entity.HotelPreviewPicture);
                entity.HotelPreviewPicture = null;
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (entity.Pictures != null)
                foreach (File file in entity.Pictures)
                    _context.Files.Remove(file);

            _context.Hotels.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
