using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCmd : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCmd>
    {
        private readonly IApplicationDbContext _context;

        public UpdateHotelCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateHotelCmd request, CancellationToken cancellationToken)
        {
            var entity = await _context.Hotels.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Hotel), request.Id);
            }

            entity.Name = request.Name ?? entity.Name;
            entity.Description = request.Description ?? entity.Description;
            entity.City = request.City ?? entity.City;
            entity.Country = request.Country ?? entity.Country;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
