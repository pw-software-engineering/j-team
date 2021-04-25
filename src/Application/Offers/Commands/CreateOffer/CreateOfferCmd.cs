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
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] OfferPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double CostPerChild { get; set; }
        public double CostPerAdult { get; set; }
        public uint MaxGuests { get; set; }
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

            var entity = new Offer
            {
                HotelId = request.HotelId,
                Hotel = hotel,
                Title = request.Title,
                Description = request.Description,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted,
                CostPerChild = request.CostPerChild,
                CostPerAdult = request.CostPerAdult,
                MaxGuests = request.MaxGuests
            };

            _context.Offers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            File previewPicture = null;
            if (request.OfferPreviewPicture != null)
            {
                previewPicture = new File
                {
                    Data = request.OfferPreviewPicture,
                    OfferId = entity.OfferId,
                    Offer = entity
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
                        OfferId = entity.OfferId,
                        Offer = entity
                    };
                    files.Add(picture);
                    _context.Files.Add(picture);
                }
            if (previewPicture != null || request.Pictures != null)
                await _context.SaveChangesAsync(cancellationToken);
            if (previewPicture != null)
            {
                entity.OfferPreviewPictureId = previewPicture.FileId;
                entity.OfferPreviewPicture = previewPicture;
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (request.Pictures != null)
            {
                entity.Pictures = files;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return entity.OfferId;
        }
    }
}
