using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelReservationSystem.Application.Hotels.Commands.CreateHotel
{
    public class CreateHotelCmd : IRequest<int>
    {
        public string Name { get; set; }
        public byte[] HotelPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class CreateHotelCmdHandler : IRequestHandler<CreateHotelCmd, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateHotelCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateHotelCmd request, CancellationToken cancellationToken)
        {
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
            var entity = new Hotel
            {
                Name = request.Name,
                HotelPreviewPicture = previewPicture,
                Pictures = pictures,
                Description = request.Description,
                City = request.City,
                Country = request.Country
            };

            _context.Hotels.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.HotelId;
        }
    }
}
