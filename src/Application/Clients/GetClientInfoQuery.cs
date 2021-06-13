using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Clients;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Clients
{
    public class GetClientInfoQuery : IRequest<ClientDto>
    {
        public int ClientId { get; set; }
    }
    public class GetClientInfoQueryHandler : IRequestHandler<GetClientInfoQuery, ClientDto>
    {
        private readonly IApplicationDbContext context;

        public GetClientInfoQueryHandler(IApplicationDbContext _context)
        {
            this.context = _context;
        }
        public async Task<ClientDto> Handle(GetClientInfoQuery request, CancellationToken cancellationToken)
        {
            var client = context.Clients.Find(request.ClientId);
            return new ClientDto()
            {
                Name = client.Name,
                Surname = client.Surname,
                Email = client.Email,
                Username = client.Username
            };
        }
    }
}