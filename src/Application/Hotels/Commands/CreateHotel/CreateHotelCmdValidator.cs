using FluentValidation;

namespace HotelReservationSystem.Application.Hotels.Commands.CreateTodoItem
{
    public class CreateHotelCmdValidator : AbstractValidator<CreateHotelCmd>
    {
        public CreateHotelCmdValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
