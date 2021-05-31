using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System;
using Application.Hotels;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Hotels.Queries.GetOfferInfo
{
    public class GetOfferInfoQuery :IRequest<DetailedOfferDto>
    {
       public int hotelId;
       public int offerId;
    }

    public class GetOfferInfoQueryHandler : IRequestHandler<GetOfferInfoQuery, DetailedOfferDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOfferInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetailedOfferDto> Handle(GetOfferInfoQuery request, CancellationToken cancellationToken)
        {
     
            var offer = _context.Offers.Where(x => x.HotelId == request.hotelId && x.OfferId == request.offerId).FirstOrDefault();
            if (offer == null) throw new NotFoundException();
            var avaibility = new List<(DateTime from, DateTime to)>();

            foreach(Room room in offer.Rooms)
            {
                if (room.Reservations.Count == 0)
                {
                    avaibility.Add((DateTime.Now, DateTime.Today.AddDays(365)));
                    continue;
                }
                room.Reservations=room.Reservations.OrderBy(x => x.FromTime).ToList();
                DateTime firstfree = DateTime.Today.AddDays(1);
                for(int i = 0; i < room.Reservations.Count; i++)
                {
                    if (room.Reservations[i].ToTime <= DateTime.Now) continue;
                    if (room.Reservations[i].FromTime > firstfree) avaibility.Add((firstfree,room.Reservations[i].FromTime.AddDays(-1)));
                    firstfree = room.Reservations[i].ToTime.AddDays(1);
                    if (i == room.Reservations.Count - 1) avaibility.Add((firstfree, DateTime.Today.AddDays(365)));
                }
            }
            
            var detailedOfferDto = new DetailedOfferDto
            {
                Title = offer.Title,
                Description = offer.Description,
                MaxGuests = offer.MaxGuests,
                CostPerAdult = offer.CostPerAdult,
                CostPerChild = offer.CostPerChild,
                OfferPicturesData = offer.Pictures,
                IsActive= offer.IsActive,
                IsDeleted=offer.IsDeleted,
                AvailabilityTimeIntervals=avaibility
                
            };
            return detailedOfferDto;
        }

    }
}
