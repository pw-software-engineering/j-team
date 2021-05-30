using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Hotels;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;

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
            var detailedOfferDto = new DetailedOfferDto
            {
                Title = offer.Title,
                Description = offer.Description,
                MaxGuests = offer.MaxGuests,
                CostPerAdult = offer.CostPerAdult,
                CostPerChild = offer.CostPerChild,
                OfferPicturesData = offer.Pictures,
                IsActive= offer.IsActive,
                IsDeleted=offer.IsDeleted

            };
            return detailedOfferDto;
        }

    }
}
