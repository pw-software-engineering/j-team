using System;
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
            var reservations = _context.Reservations
                .OrderBy(x => x.ReservationId)
                .Where(x => x.ClientId == request.ClientId)
                .ToList();
            var result = new List<ClientReservationResult>();
            foreach (var reservation in reservations)
            {
                var clientReservationResult = new ClientReservationResult();
                clientReservationResult.reservationInfo = new ReservationInfo
                {
                    reservationID = reservation.ReservationId,
                    from = reservation.FromTime,
                    to = reservation.ToTime,
                    numberOfAdults = reservation.AdultsCount,
                    numberOfChildren = reservation.ChildrenCount,
                    reviewID = _context.Reviews
                        .Where(r => r.ClientId == request.ClientId && r.OfferId == reservation.OfferId)
                        .Select(r => r.ReviewId)
                        .FirstOrDefault()
                };
                var offer = await _context.Offers.FindAsync(reservation.OfferId);
                var picture = await _context.Files.FindAsync(offer.OfferPreviewPicture);
                clientReservationResult.offerInfoPreview = new OfferInfoPreview
                {
                    offerID = offer.OfferId,
                    offerPreviewPicture = picture != null ? picture.Data : null,
                    offerTitle = offer.Title
                };
                var hotel = await _context.Hotels.FindAsync(offer.HotelId);
                clientReservationResult.hotelInfoPreview = new HotelInfoPreview
                {
                    city = hotel.City,
                    country = hotel.Country,
                    hotelID = hotel.HotelId,
                    hotelName = hotel.Name
                };
                result.Add(clientReservationResult);
            }
            return result;
        }
    }
}

public class ClientReservationResult
{
    public HotelInfoPreview hotelInfoPreview { get; set; }
    public ReservationInfo reservationInfo { get; set; }
    public OfferInfoPreview offerInfoPreview { get; set; }
}

public class HotelInfoPreview
{
    public int hotelID { get; set; }
    public string hotelName { get; set; }
    public string country { get; set; }
    public string city { get; set; }
}

public class ReservationInfo  
{
    public int reservationID { get; set; }
    public DateTime from { get; set; }
    public DateTime to { get; set; }
    public int numberOfChildren { get; set; }
    public int numberOfAdults { get; set; }
    public int? reviewID { get; set; }
}

public class OfferInfoPreview
{
    public int offerID { get; set; }
    public string offerTitle { get; set; }
    public byte[] offerPreviewPicture { get; set; }
}