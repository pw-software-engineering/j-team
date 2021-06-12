using System.Threading;
using System.Threading.Tasks;
using Application.Hotels;
using AutoMapper;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelInfo;
using MediatR;

namespace HotelReservationSystem.Application.Hotels.Queries
{
    public class GetHotelQuery : IRequest<HotelDetailsDto>
    {
        public int HotelId { get; set; }
    }
    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, HotelDetailsDto>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GetHotelQueryHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<HotelDetailsDto> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var res = await mediator.Send(new GetHotelInfoQuery() { hotelId = request.HotelId });
            return mapper.Map<HotelDto, HotelDetailsDto>(res);
        }
    }
}