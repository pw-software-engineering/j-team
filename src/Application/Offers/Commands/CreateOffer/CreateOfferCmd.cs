using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelReservationSystem.Application.Common.Exceptions;

namespace HotelReservationSystem.Application.Offers.Commands.CreateOffer
{
    public class CreateOfferCmd : IRequest<int>
    {
        public int HotelId { get; set; }
        public string OfferTitle { get; set; }
        public string Description { get; set; }
        public byte[] OfferPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public uint MaxGuests { get; set; }
        public List<Room> Rooms { get; set; }
    }

    public class CreateOfferCmdHandler : IRequestHandler<CreateOfferCmd, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOfferCmdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOfferCmd request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FindAsync(request.HotelId);

            if (hotel == null)
            {
                throw new NotFoundException(nameof(Hotel), request.HotelId);
            }
            PreviewFile previewPicture = new PreviewFile
            {
                Data = request.OfferPreviewPicture
            };
            _context.PreviewFiles.Add(previewPicture);
            await _context.SaveChangesAsync(cancellationToken);

            List<File> pictures = new List<File>();
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

            var entity = new Offer
            {
                HotelId = request.HotelId,
                Hotel = hotel,
                Title = request.OfferTitle,
                Description = request.Description,
                OfferPreviewPictureId = previewPicture.FileId,
                OfferPreviewPicture = previewPicture,
                Pictures = pictures,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted,
                CostPerChild = request.CostPerChild,
                CostPerAdult = request.CostPerAdult,
                MaxGuests = request.MaxGuests,
                Rooms = request.Rooms
            };

            _context.Offers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.OfferId;
        }
    }
}
