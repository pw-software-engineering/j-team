using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Hotels.Commands.CreateTodoItem
{
    public class CreateHotelCmd : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }
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
            };

            _context.Hotels.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
