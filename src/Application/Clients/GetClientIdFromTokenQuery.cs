using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Application.Clients.Commands.GetClientToken
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