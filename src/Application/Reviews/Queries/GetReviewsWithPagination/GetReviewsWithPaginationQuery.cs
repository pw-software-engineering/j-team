using Application.Common.Models;
using Application.Offers;
using Application.Reviews;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Reviews.Queries.GetReviewsWithPaginationQuery
{
    public class GetReviewsWithPaginationQuery : PageableQuery<PaginatedList<ReviewDto>>
    {
        public int HotelID { get; set; }
        public int OfferID { get; set; }
    }

    public class GetReviewsWithPaginationQueryHandler : IRequestHandler<GetReviewsWithPaginationQuery, PaginatedList<ReviewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReviewDto>> Handle(GetReviewsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var offer = _context.Offers.Find(request.OfferID);
            if (offer is null || offer.HotelId != request.HotelID)
                throw new NotFoundException();

            return await _context.Reviews
                .Where(x => x.OfferId == request.OfferID)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
