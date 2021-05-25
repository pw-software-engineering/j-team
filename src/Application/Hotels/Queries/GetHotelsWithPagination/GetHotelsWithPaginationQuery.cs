using Application.Hotels;
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

namespace HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination
{
    public class GetHotelsWithPaginationQuery : IRequest<PaginatedList<HotelListedDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string Country { get; set; } = null;
        public string City { get; set; } = null;
        public string HotelName { get; set; } = null;
    }

    public class GetHotelsWithPaginationQueryHandler : IRequestHandler<GetHotelsWithPaginationQuery, PaginatedList<HotelListedDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHotelsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<HotelListedDto>> Handle(GetHotelsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Hotels
                .OrderBy(x => x.Name)
                .Where(x => string.IsNullOrWhiteSpace(request.HotelName) || x.Name.ToLower().StartsWith(request.HotelName.ToLower()))
                .Where(x => string.IsNullOrWhiteSpace(request.City) || x.City.ToLower().StartsWith(request.City.ToLower()))
                .Where(x => string.IsNullOrWhiteSpace(request.Country) || x.Country.ToLower().StartsWith(request.Country.ToLower()))
                .ProjectTo<HotelListedDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
