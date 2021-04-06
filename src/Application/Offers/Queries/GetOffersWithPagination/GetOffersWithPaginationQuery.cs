using Application.Offers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination
{
    public class GetOffersWithPaginationQuery : IRequest<PaginatedList<OfferDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetOffersWithPaginationQueryHandler : IRequestHandler<GetOffersWithPaginationQuery, PaginatedList<OfferDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOffersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OfferDto>> Handle(GetOffersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Offers
                .OrderBy(x => x.Title)
                .ProjectTo<OfferDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize); ;
        }
    }
}
