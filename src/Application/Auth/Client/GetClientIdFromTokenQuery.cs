using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth
{
    public class GetClientIdFromTokenQuery : IRequest<int?>
    {
        public string Token { get; set; }
    }

    public class GetClientIdFromTokenQueryHandler : IRequestHandler<GetClientIdFromTokenQuery, int?>
    {
        private readonly IApplicationDbContext _context;

        public GetClientIdFromTokenQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Handle(GetClientIdFromTokenQuery request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.AccessToken == request.Token);

            return client?.ClientId;
        }
    }
}