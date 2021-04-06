using FluentValidation;

namespace HotelReservationSystem.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCmdValidator : AbstractValidator<UpdateHotelCmd>
    {
        public UpdateHotelCmdValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
