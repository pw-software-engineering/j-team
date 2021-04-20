using System.Linq;
using FluentValidation;
using HotelReservationSystem.Application.Common.Interfaces;

namespace HotelReservationSystem.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCmdValidator : AbstractValidator<CreateRoomCmd>
    {
        public CreateRoomCmdValidator(IApplicationDbContext dbContext)
        {
            RuleFor(v => v.HotelRoomNumber)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(cmd => cmd).Custom((cmd, context) =>
            {
                if (dbContext.Rooms
                .Where(x => x.HotelId == cmd.HotelID)
                .Any(x => x.HotelRoomNumber == cmd.HotelRoomNumber))
                {
                    context.AddFailure("Provided hotel room number already exists in database.");
                }
            });
        }
    }
}
