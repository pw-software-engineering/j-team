using Application.Hotels;
using Application.Offers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Hotels.Queries.GetFilteredHotelOffers
{
    public class GetFilteredHotelOffersQuery : IRequest<List<OfferDto>>
    {
        public int HotelId { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public int? MinGuest { get; set; }
        public int? CostMin { get; set; }
        public int? CostMax { get; set; }
    }

    public class GetFilteredHotelOffersQueryHandler : IRequestHandler<GetFilteredHotelOffersQuery, List<OfferDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFilteredHotelOffersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OfferDto>> Handle(GetFilteredHotelOffersQuery request, CancellationToken cancellationToken)
        {
            var hotel = _context.Hotels.Where(x => x.HotelId == request.HotelId).FirstOrDefault();
            if (hotel == null)
                throw new ValidationException();
            return await _context.Offers
                .Where(x =>
                    x.HotelId == request.HotelId &&
                    x.IsActive.Value &&
                    (request.FromTime == null || request.ToTime == null || x.Rooms.Where(r => r.Reservations
                        .All(r => !((DateTime.Compare(r.FromTime, request.FromTime.GetValueOrDefault()) < 0 &&
                                    DateTime.Compare(r.ToTime, request.FromTime.GetValueOrDefault()) > 0) ||
                                   (DateTime.Compare(r.FromTime, request.ToTime.GetValueOrDefault()) < 0 &&
                                    DateTime.Compare(r.ToTime, request.ToTime.GetValueOrDefault()) > 0)))).Any()) &&
                    (request.MinGuest == null || x.MaxGuests >= request.MinGuest) &&
                    (request.CostMin == null || x.CostPerAdult >= request.CostMin) &&
                    (request.CostMax == null || x.CostPerAdult <= request.CostMax))
                .ProjectToListAsync<OfferDto>(_mapper.ConfigurationProvider);
        }
    }
}
