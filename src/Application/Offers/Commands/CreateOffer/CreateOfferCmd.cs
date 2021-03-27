using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelReservationSystem.Application.Offers.Commands.CreateOffer
{
    public class CreateOfferCmd : IRequest<int>
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
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
            var entity = new Offer
            {
                OfferId = request.OfferId,
                Title = request.Title,
                OfferPreviewPicture = request.OfferPreviewPicture,
                Pictures = request.Pictures,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted,
                CostPerChild = request.CostPerChild,
                CostPerAdult = request.CostPerAdult,
                MaxGuests = request.MaxGuests
            };

            _context.Offers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.OfferId;
        }
    }
}
