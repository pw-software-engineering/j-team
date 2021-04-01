using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
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
            var entity = await _context.Offers.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Offer), request.Id);
            }



            _context.Offers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
