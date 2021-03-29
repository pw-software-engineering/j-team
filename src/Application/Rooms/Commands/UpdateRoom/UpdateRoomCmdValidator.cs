using FluentValidation;

namespace HotelReservationSystem.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCmdValidator : AbstractValidator<UpdateRoomCmd>
    {
        public UpdateRoomCmdValidator()
        {
            RuleFor(v => v.HotelRoomNumber)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
