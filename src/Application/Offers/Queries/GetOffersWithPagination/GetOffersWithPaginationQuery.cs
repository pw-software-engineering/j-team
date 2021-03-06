﻿using Application.Common.Models;
using Application.Offers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination
{
    public class GetOffersWithPaginationQuery : PageableQuery<PaginatedList<OfferDto>>
    {
        public bool? IsActive { get; set; } = null;
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
                .Where(x => request.IsActive == null || request.IsActive == x.IsActive)
                .ProjectTo<OfferDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
