using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Clients.Commands.CreateClient
{
    public class GetClientIdFromTokenQuery : IRequest<int>
    {
        public string Token { get; set; }
    }
    public class GetClientIdFromTokenQueryHandler : IRequestHandler<GetClientIdFromTokenQuery, int>
    {
        private readonly IApplicationDbContext _context;

        public GetClientIdFromTokenQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetClientIdFromTokenQuery request, CancellationToken cancellationToken)
        {
            //TODO: zmienic na token prawdziwy
            return _context.Clients.First().ClientId;
        }
    }
}