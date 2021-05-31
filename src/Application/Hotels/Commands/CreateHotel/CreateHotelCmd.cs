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
        public string Password { get; set; }
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
            var entity = new Hotel
            {
                Name = request.Name,
                Description = request.Description,
                City = request.City,
                Country = request.Country,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                AccessToken = BCrypt.Net.BCrypt.HashPassword(request.Password + request.Name)
            };

            _context.Hotels.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            File previewPicture = null;
            if (request.HotelPreviewPicture != null)
            {
                previewPicture = new File
                {
                    Data = request.HotelPreviewPicture,
                    HotelId = entity.HotelId,
                    Hotel = entity
                };
                _context.Files.Add(previewPicture);
            }
            List<File> files = new List<File>();
            if (request.Pictures != null)
                foreach (var file in request.Pictures)
                {
                    File picture = new File
                    {
                        Data = file,
                        HotelId = entity.HotelId,
                        Hotel = entity
                    };
                    files.Add(picture);
                    _context.Files.Add(picture);
                }
            if (previewPicture != null || request.Pictures != null)
                await _context.SaveChangesAsync(cancellationToken);
            if (previewPicture != null)
            {
                entity.HotelPreviewPictureId = previewPicture.FileId;
                entity.HotelPreviewPicture = previewPicture;
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (request.Pictures != null)
            {
                entity.Pictures = files;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return entity.HotelId;
        }
    }
}
