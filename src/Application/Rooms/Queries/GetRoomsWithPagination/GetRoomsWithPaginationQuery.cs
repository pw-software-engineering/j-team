using Application.Rooms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination
{
    public class GetRoomsWithPaginationQuery : IRequest<PaginatedList<RoomDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string RoomNumber { get; set; } = null;
    }

    public class GetRoomsWithPaginationQueryHandler : IRequestHandler<GetRoomsWithPaginationQuery, PaginatedList<RoomDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<RoomDto>> Handle(GetRoomsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .OrderBy(x => x.HotelRoomNumber)
                .Where(x => string.IsNullOrEmpty(request.RoomNumber)
                || request.RoomNumber == x.HotelRoomNumber)
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
