
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Reviews.Cmd.CreateRevewCmd
{
    public class CreateReviewCmd : IRequest<int>
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public int HotelId { get; set; }
        public int OfferId { get; set; }
        public int ClientId { get; set; }
    }
    public class CreateReviewResponse
    {
        public int ReviewID { get; set; }
        public string Error { get; set; }
    }

    public class CreateReviewCmdHandler : IRequestHandler<CreateReviewCmd, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateReviewCmdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateReviewCmd request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
                throw new ValidationException("Content must be non empty");
            if (request.Rating > 5 || request.Rating < 1)
                throw new ValidationException("Rating must be from 1 to 5");

            var offer = _context.Offers.Find(request.OfferId);
            if (offer is null || offer.HotelId != request.HotelId)
                throw new NotFoundException();

            var review = new Review()
            {
                Content = request.Content,
                Rating = request.Rating,
                OfferId = request.OfferId,
                ClientId = request.ClientId
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync(cancellationToken);

            return review.ReviewId;
        }
    }
}