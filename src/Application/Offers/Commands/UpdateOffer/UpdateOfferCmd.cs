using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCmd : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] OfferPreviewPicture { get; set; }
        public List<byte[]> Pictures { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public double? CostPerChild { get; set; }
        public double? CostPerAdult { get; set; }
        public uint? MaxGuests { get; set; }
    }

    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCmd>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOfferCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOfferCmd request, CancellationToken cancellationToken)
        {
            var entity = await _context.Offers.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Offer), request.Id);
            }
            PreviewFile previewPicture = null;
            if (request.OfferPreviewPicture != null)
            {
                previewPicture = new PreviewFile
                {
                    Data = request.OfferPreviewPicture
                };
                _context.PreviewFiles.Add(previewPicture);
                await _context.SaveChangesAsync(cancellationToken);
            }

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

            entity.Title = request.Title ?? entity.Title;
            entity.Description = request.Description ?? entity.Description;
            if (request.OfferPreviewPicture != null)
            {
                entity.OfferPreviewPictureId = previewPicture.FileId;
                entity.OfferPreviewPicture = previewPicture;
            }
            entity.Pictures = request.Pictures == null ? entity.Pictures : pictures;
            if (request.IsActive != null)
                entity.IsActive = request.IsActive.Value;
            if (request.IsDeleted != null)
                entity.IsDeleted = request.IsDeleted.Value;
            if (request.CostPerChild != null)
                entity.CostPerChild = request.CostPerChild.Value;
            if (request.CostPerAdult != null)
                entity.CostPerAdult = request.CostPerAdult.Value;
            if (request.MaxGuests != null)
                entity.MaxGuests = request.MaxGuests.Value;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
