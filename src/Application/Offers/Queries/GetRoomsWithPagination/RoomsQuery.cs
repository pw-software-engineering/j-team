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
    public class RoomsQuery : IRequest<PaginatedList<RoomDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int OfferId;
        public int HotelId = 1; // finalnie będzie czytane z tokena
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
            //if (offer.HotelId != request.HotelId) // TODO po czytaniu z tokena
            //    throw new ForbiddenAccessException();
            return offer.Rooms
                .AsQueryable()
                .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .GetPaginatedList(request.PageNumber, request.PageSize);
        }
    }
}
