using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Commands.DeleteOffer
{
    public class DeleteOfferCmd : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteOfferCmdHandler : IRequestHandler<DeleteOfferCmd>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOfferCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOfferCmd request, CancellationToken cancellationToken)
        {
            var entity = _context.Offers
                .Include(x => x.OfferPreviewPicture)
                .Include(x => x.Pictures)
                .FirstOrDefault(r => r.OfferId == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Offer), request.Id);
            }

            if (entity.OfferPreviewPicture != null)
            {
                _context.Files.Remove(entity.OfferPreviewPicture);
                entity.OfferPreviewPictureId = null;
                await _context.SaveChangesAsync(cancellationToken);
            }

            foreach (File file in entity.Pictures)
                _context.Files.Remove(file);
            
            _context.Offers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
