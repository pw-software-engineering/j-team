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
        public int Id;
        public string hotelName { get; set; }
        public string hotelDesc { get; set; }
        public File hotelPreviewPicture { get; set; }
        public List<File> hotelPictures { get; set; }
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

            entity.Name = request.hotelName ?? entity.Name;
            entity.Description = request.hotelDesc ?? entity.Description;
            entity.HotelPreviewPicture = request.hotelPreviewPicture ?? entity.HotelPreviewPicture;
            entity.Pictures = request.hotelPictures ?? entity.Pictures;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
