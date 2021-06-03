using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth
{
    public class ClientLoginCmd : IRequest<string>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class AuthorizeClientQueryHandler : IRequestHandler<ClientLoginCmd, string>
    {
        private readonly IApplicationDbContext context;

        public AuthorizeClientQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> Handle(ClientLoginCmd request, CancellationToken cancellationToken)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Username == request.Login);
            if (client == null)
                throw new ForbiddenAccessException();
            if (BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
            {
                var token = JsonSerializer.Serialize(new ClientToken
                {
                    id = client.ClientId,
                    createdAt = DateTime.UtcNow
                });
                client.AccessToken = token;
                await context.SaveChangesAsync(cancellationToken);
                return client.AccessToken;
            }
            throw new ForbiddenAccessException();
        }
    }
    public class ClientToken
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
    }
}