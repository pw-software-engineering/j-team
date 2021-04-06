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
        public byte[] HotelPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
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
            if (request.Name != null)
                entity.Name = request.Name;
            if (request.HotelPreviewPicture != null)
                entity.HotelPreviewPicture = request.HotelPreviewPicture;
            if (request.Pictures != null)
                entity.Pictures = request.Pictures;
            if (request.Description != null)
                entity.Description = request.Description;
            if (request.City != null)
                entity.City = request.City;
            if (request.Country != null)
                entity.Country = request.Country;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
