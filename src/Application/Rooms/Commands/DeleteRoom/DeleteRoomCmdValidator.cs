using System.Linq;
using FluentValidation;
using HotelReservationSystem.Application.Common.Interfaces;

namespace HotelReservationSystem.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCmdValidator : AbstractValidator<DeleteRoomCmd>
    {
        public DeleteRoomCmdValidator(IApplicationDbContext dbContext)
        {
            RuleFor(v => v.Id)
                .GreaterThan(0);
        }
    }
}
