using FluentValidation;

namespace HotelReservationSystem.Application.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCmdValidator : AbstractValidator<UpdateOfferCmd>
    {
        public UpdateOfferCmdValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
