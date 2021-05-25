using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCmd : IRequest
    {
        public int Id;
        public string hotelName { get; set; }
        public string hotelDesc { get; set; }
        public byte[] hotelPreviewPicture { get; set; }
        public List<byte[]> hotelPictures { get; set; }
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

            if (request.hotelPreviewPicture != null)
            {
                entity.HotelPreviewPicture = addPicture(request.hotelPreviewPicture, entity);
                entity.HotelPreviewPictureId = entity.HotelPreviewPicture?.FileId;
            }
            if (request.hotelPictures != null)
            {
                entity.Pictures = request.hotelPictures.Select(x => addPicture(x, entity)).ToList();
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        File addPicture(byte[] file, Hotel entity)
        {
            var previewPicture = new File
            {
                Data = file,
                HotelId = entity.HotelId
            };
            _context.Files.Add(previewPicture);
            return previewPicture;
        }
    }
}
