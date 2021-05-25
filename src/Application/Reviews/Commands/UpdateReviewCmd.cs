
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Reviews.Cmd.UpdateRevewCmd
{
    public class UpdateReviewCmd : IRequest<int>
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public string ReviewerUsername { get; set; }
        public int ReviewID { get; set; }
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public int ClientId { get; set; }
    }

    public class UpdateReviewCmdHandler : IRequestHandler<UpdateReviewCmd, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateReviewCmdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateReviewCmd request, CancellationToken cancellationToken)
        {
            var offer = _context.Offers.Find(request.OfferId);
            var review = _context.Reviews.Find(request.ReviewID);
            if (offer is null || offer.HotelId != request.HotelId || review is null)
                throw new NotFoundException();

            if (review.ClientId != request.ClientId)
                throw new ForbiddenAccessException();

            review.Content = string.IsNullOrWhiteSpace(request.Content) ? review.Content : request.Content;
            review.Rating = request.Rating;
            review.ReviewDate = request.CreationDate;

            await _context.SaveChangesAsync(cancellationToken);

            return review.ReviewId;
        }
    }
}