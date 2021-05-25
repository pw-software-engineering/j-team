using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reviews.Commands
{
    public class DeleteReviewCmd : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public int OfferId { get; set; }
        public int ReviewId { get; set; }
    }
    public class DeleteReviewCmdHandler : IRequestHandler<DeleteReviewCmd, Unit>
    {
        private readonly IApplicationDbContext context;

        public DeleteReviewCmdHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteReviewCmd request, CancellationToken cancellationToken)
        {
            var offer = await context.Offers.Include(x => x.Reviews).FirstOrDefaultAsync(x => x.OfferId == request.OfferId);
            var review = context.Reviews.Find(request.ReviewId);
            if (offer is null || review is null || offer.HotelId != request.HotelId)
                throw new NotFoundException();
            if (review.ClientId != request.ClientId)
                throw new ForbiddenAccessException();

            context.Reviews.Remove(review);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}