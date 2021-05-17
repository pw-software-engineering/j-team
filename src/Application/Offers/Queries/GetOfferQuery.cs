using Application.Offers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Queries
{
    public class GetOfferQuery : IRequest<OfferDto>
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
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
            var offer = await _context.Offers.FindAsync(request.Id);
            if (offer is null)
                throw new NotFoundException();
            if (offer.HotelId != request.HotelId)
                throw new ForbiddenAccessException();
            return _mapper.Map<OfferDto>(offer);
        }
    }
}
