using Application.Common.Models;
using Application.Rooms;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Exceptions;
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

namespace HotelReservationSystem.Application.Offers.Queries.Rooms
{
    public class RoomsQuery : PageableQuery<PaginatedList<RoomDto>>
    {
        public int OfferId;
        public int HotelId;
        public string RoomNumber { get; set; }
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
            var offer = _context.Offers.Include(x => x.Rooms).FirstOrDefault(x => x.OfferId == request.OfferId);
            if (offer == null)
                throw new NotFoundException(nameof(Domain.Entities.Offer), request.OfferId); ;
            if (offer.HotelId != request.HotelId)
                throw new ForbiddenAccessException();

            var response = offer.Rooms.Where(x => string.IsNullOrWhiteSpace(request.RoomNumber)
            || x.HotelRoomNumber == request.RoomNumber);

            if (response.Count() == 0 && !string.IsNullOrWhiteSpace(request.RoomNumber))
                throw new NotFoundException(nameof(Domain.Entities.Offer), request.OfferId);

            return response
                .AsQueryable()
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .GetPaginatedList(request.PageNumber, request.PageSize);
        }
    }
}
