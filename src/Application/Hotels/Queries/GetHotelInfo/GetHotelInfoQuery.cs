using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Offers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Hotels.Queries.GetFilteredHotelOffers;
using MediatR;

namespace Application.Hotels.Queries.GetHotelInfo
{
    public class GetHotelInfoQuery : IRequest<HotelDto>
    {
        public int hotelId;
    }

    public class GetHotelInfoQueryHandler : IRequestHandler<GetHotelInfoQuery, HotelDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHotelInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HotelDto> Handle(GetHotelInfoQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FindAsync(request.hotelId);
            var hotelDto = new HotelDto
            {
                City = hotel.City,
                Country = hotel.Country,
                HotelDesc = hotel.Description,
                HotelName = hotel.Name,
                HotelPreviewPictureData = hotel.HotelPreviewPicture,
                PicturesData = hotel.Pictures
            };
            return hotelDto;
        }
        
    }
}