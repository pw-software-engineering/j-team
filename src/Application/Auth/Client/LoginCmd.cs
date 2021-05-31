using System;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth
{
    public class LoginCmd : IRequest<ClientSessionToken>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class ClientSessionToken
    {
        public int Id;
        public DateTime CreatedAt;
    }
    public class AuthorizeClientQueryHandler : IRequestHandler<LoginCmd, ClientSessionToken>
    {
        private readonly IApplicationDbContext context;

        public AuthorizeClientQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ClientSessionToken> Handle(LoginCmd request, CancellationToken cancellationToken)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Email == request.Login);
            if (client == null)
                throw new ForbiddenAccessException();
            if (BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
            {
                return new ClientSessionToken
                {
                    Id = client.ClientId,
                    CreatedAt = DateTime.Now
                };
            }
            throw new ForbiddenAccessException();
        }
    }
}