using Application.Rooms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Queries.Rooms
{
    public class RoomsQuery : IRequest<PaginatedList<RoomDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int OfferId = 1;
    }

    public class RoomsQueryHandler : IRequestHandler<RoomsQuery, PaginatedList<RoomDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoomsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<RoomDto>> Handle(RoomsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .Where(r => r.Offers.Any(o => o.OfferId == request.OfferId))
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
