using FluentValidation;

namespace HotelReservationSystem.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCmdValidator : AbstractValidator<CreateRoomCmd>
    {
        public CreateRoomCmdValidator()
        {
            RuleFor(v => v.HotelRoomNumber)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
