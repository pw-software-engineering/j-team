using System.Collections.Generic;
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
using Application.Hotels;
using Application.Offers;
using HotelReservationSystem.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Application.Reservations.Queries.GetReservationsWithPagination
{

    public class GetClientReservationsWithPaginationQuery : IRequest<List<ClientReservationResult>>
    {
        public int ClientId;
    }

    public class GetClientReservationsWithPaginationQueryHandler : IRequestHandler<GetClientReservationsWithPaginationQuery, List<ClientReservationResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientReservationsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClientReservationResult>> Handle(GetClientReservationsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var now = System.DateTime.Now;
            return _context.Reservations
                .OrderBy(x => x.ReservationId)
                .Where(x => x.ClientId == request.ClientId)
                .ProjectTo<ClientReservationResult>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}

public class ClientReservationResult
{
    public HotelDto hotelInfoPreview { get; set; }
    public ReservationDto reservationInfo { get; set; }
    public OfferDto offerInfoPreview { get; set; }
}

public class HotelInfoPreview
{
    public int hotelID { get; set; }
    public string hotelName { get; set; }
    public string country { get; set; }
    public string city { get; set; }
}