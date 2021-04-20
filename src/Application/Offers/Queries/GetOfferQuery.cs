using Application.Offers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Queries
{
    public class GetOfferQuery : IRequest<OfferDto>
    {
        public int Id { get; set; }
    }

    public class GetOfferQueryHandler : IRequestHandler<GetOfferQuery, OfferDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOfferQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OfferDto> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            var findTask = Task.Run(() => _context.Offers.Where(x => x.OfferId == request.Id).ProjectTo<OfferDto>(_mapper.ConfigurationProvider).SingleOrDefault());
            return await findTask;
        }
    }
}
