using FluentValidation;

namespace HotelReservationSystem.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCmdValidator : AbstractValidator<UpdateHotelCmd>
    {
        public UpdateHotelCmdValidator()
        {
            RuleFor(v => v.hotelName)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
