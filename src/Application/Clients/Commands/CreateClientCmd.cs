using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Clients.Commands.CreateClient
{
    public class CreateClientCmd : IRequest<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
     public class CreateClientCmdHandler : IRequestHandler<CreateClientCmd, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateClientCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateClientCmd request, CancellationToken cancellationToken)
        {
            var entity = new Client
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                Email = request.Email
            };
            _context.Clients.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.ClientId;
        }
    }
}