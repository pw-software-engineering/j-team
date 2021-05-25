using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth
{
    public class AuthorizeHotelQuery : IRequest<string>
    {
        public string HotelName { get; set; }
        public string Password { get; set; }
    }
    public class AuthorizeHotelQueryHandler : IRequestHandler<AuthorizeHotelQuery, string>
    {
        private readonly IApplicationDbContext context;

        public AuthorizeHotelQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> Handle(AuthorizeHotelQuery request, CancellationToken cancellationToken)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(x => x.Name == request.HotelName);
            if (BCrypt.Net.BCrypt.Verify(request.Password, hotel.Password))
            {
                return hotel.AccessToken;
            }
            return null;
        }
    }
}