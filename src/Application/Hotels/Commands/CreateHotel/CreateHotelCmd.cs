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
            var entity = new Hotel
            {
                Name = request.Name,
                HotelPreviewPicture = request.HotelPreviewPicture,
                Pictures = request.Pictures,
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
