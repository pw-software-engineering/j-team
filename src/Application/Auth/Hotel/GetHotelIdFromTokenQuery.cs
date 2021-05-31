using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth
{
    public class GetHotelIdFromTokenQuery : IRequest<int?>
    {
        public string Token { get; set; }
    }

    public class GetHotelIdFromTokenQueryHandler : IRequestHandler<GetHotelIdFromTokenQuery, int?>
    {
        private readonly IApplicationDbContext _context;

        public GetHotelIdFromTokenQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Handle(GetHotelIdFromTokenQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.AccessToken == request.Token);

            return hotel?.HotelId;
        }
    }
}