using FluentValidation;

namespace HotelReservationSystem.Application.Hotels.Commands.CreateHotel
{
    public class CreateHotelCmdValidator : AbstractValidator<CreateHotelCmd>
    {
        public CreateHotelCmdValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Country)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.City)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
