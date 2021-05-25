
using Application.Common.Models;
using Application.Rooms;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination
{

    

    public class GetRoomsWithPaginationQuery :  PageableQuery<PaginatedList<RoomDto>>
    {

       
        public int HotelId;
#nullable enable
        public string? RoomNumber { get; set; } = null;
#nullable disable
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
                .Where(x => request.HotelId == x.HotelId)
                 .Where(x => request.RoomNumber == null || request.RoomNumber == x.HotelRoomNumber)
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
