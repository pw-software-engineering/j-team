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

            entity.Title = request.Title == null ? entity.Title : request.Title;
            entity.OfferPreviewPicture = request.OfferPreviewPicture == null ? entity.OfferPreviewPicture : request.OfferPreviewPicture;
            entity.Pictures = request.Pictures == null ? entity.Pictures : request.Pictures;
            if (request.IsActive != null)
                entity.IsActive = (bool)request.IsActive;
            if (request.IsDeleted != null)
                entity.IsDeleted = (bool)request.IsDeleted;
            if (request.CostPerChild != null)
                entity.CostPerChild = (double)request.CostPerChild;
            if (request.CostPerAdult != null)
                entity.CostPerAdult = (double)request.CostPerAdult;
            if (request.MaxGuests != null)
                entity.MaxGuests = (uint)request.MaxGuests;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
