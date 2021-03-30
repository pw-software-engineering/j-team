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

            PreviewFile previewPicture = null;
            if (request.HotelPreviewPicture != null)
            {
                previewPicture = new PreviewFile
                {
                    Data = request.HotelPreviewPicture
                };
                _context.PreviewFiles.Add(previewPicture);
                await _context.SaveChangesAsync(cancellationToken);
            }

            List<File> pictures = null;
            if (request.Pictures.Count > 0)
            {
                pictures = new List<File>();
                foreach (var picture in request.Pictures)
                {
                    File tmpPicture = new File
                    {
                        Data = picture
                    };
                    _context.Files.Add(tmpPicture);
                    await _context.SaveChangesAsync(cancellationToken);
                    pictures.Add(tmpPicture);
                }
            }

            if (request.Name != null)
                entity.Name = request.Name;
            if (request.HotelPreviewPicture != null)
            {
                entity.HotelPreviewPicture = previewPicture;
            }
            entity.Pictures = request.Pictures == null ? entity.Pictures : pictures;
            entity.Description = request.Description ?? entity.Description;
            request.City = entity.City ?? request.City;
            request.Country = entity.Country ?? request.Country;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
