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

            RuleFor(cmd => cmd).Custom((cmd, context) =>
            {
                if (dbContext.Rooms
                .Where(x => x.HotelId == cmd.HotelId)
                .Any(x => x.RoomId == cmd.Id))
                {
                    context.AddFailure($"Hotel room with id {cmd.Id} was not found.");
                }
            });
        }
    }
}
