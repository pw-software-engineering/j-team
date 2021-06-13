using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using MediatR;

namespace Application.Clients.Commands
{
    public class UpdateClientCmd : IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
    public class UpdateClientResponse
    {
        public string ErrorDescription { get; set; }
    }
    public class UpdateClientCmdHandler : IRequestHandler<UpdateClientCmd, Unit>
    {
        private readonly IApplicationDbContext context;

        public UpdateClientCmdHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Unit> Handle(UpdateClientCmd request, CancellationToken cancellationToken)
        {
            var client = context.Clients.Find(request.Id);
            if (!String.IsNullOrEmpty(request.Email) && !request.Email.Contains("@"))
                throw new ValidationException();

            client.Username = string.IsNullOrEmpty(request.Username) ? client.Username : request.Username;
            if (!string.IsNullOrEmpty(request.Email))
                client.Email = request.Email;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}