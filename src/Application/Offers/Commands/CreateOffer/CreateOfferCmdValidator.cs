using FluentValidation;

namespace HotelReservationSystem.Application.Offers.Commands.CreateOffer
{
    public class CreateOfferCmdValidator : AbstractValidator<CreateOfferCmd>
    {
        public CreateOfferCmdValidator()
        {
            RuleFor(v => v.OfferTitle)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
