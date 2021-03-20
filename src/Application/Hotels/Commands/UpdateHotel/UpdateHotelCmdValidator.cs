using FluentValidation;

namespace HotelReservationSystem.Application.Hotels.Commands.UpdateTodoItem
{
    public class UpdateHotelCmdValidator : AbstractValidator<UpdateHotelCmd>
    {
        public UpdateHotelCmdValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
