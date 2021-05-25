using Application.Rooms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using MediatR;
using Application.Reservations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;

namespace HotelReservationSystem.Application.Reservations.Queries.GetReservationsWithPagination
{

    public class GetReservationsWithPaginationQuery : IRequest<PaginatedList<ReservationDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int HotelId { get; set; }
#nullable enable
        public int? RoomID { get; set; } = null;
        public bool? CurrentOnly { get; set; } = null;

#nullable disable
    }

    public class GetReservationsWithPaginationQueryHandler : IRequestHandler<GetReservationsWithPaginationQuery, PaginatedList<ReservationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReservationsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReservationDto>> Handle(GetReservationsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            if (request.RoomID != null)
            {
                var reservation = _context.Rooms.FirstOrDefault(x => x.RoomId == request.RoomID);
                if (reservation.HotelId != request.HotelId)
                {
                    throw new ForbiddenAccessException();
                }
            }

            return await _context.Reservations
                .OrderBy(x => x.ReservationId)
                 .Where(x => request.RoomID == null || request.RoomID == x.RoomId)
                 .Where(x => request.CurrentOnly==null || (request.CurrentOnly == true&& System.DateTime.Now>x.FromTime))
                .ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

		
	}
}
