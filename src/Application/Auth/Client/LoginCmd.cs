using System;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth
{
    public class LoginCmd : IRequest<string>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class ClientSessionToken
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class AuthorizeClientQueryHandler : IRequestHandler<LoginCmd, string>
    {
        private readonly IApplicationDbContext context;

        public AuthorizeClientQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> Handle(LoginCmd request, CancellationToken cancellationToken)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Email == request.Login);
            if (client == null)
                throw new ForbiddenAccessException();
            if (BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
            {
                /*var token = new ClientSessionToken
                {
                    Id = client.ClientId,
                    CreatedAt = DateTime.Now.ToLocalTime()
                };*/
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", client.ClientId.ToString()), new Claim("createdAt", DateTime.UtcNow.ToString()) })
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                client.AccessToken = tokenHandler.WriteToken(token);
                await context.SaveChangesAsync(cancellationToken);
                return client.AccessToken;
            }
            throw new ForbiddenAccessException();
        }
    }
}