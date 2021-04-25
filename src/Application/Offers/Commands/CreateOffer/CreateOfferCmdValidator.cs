using FluentValidation;

namespace HotelReservationSystem.Application.Offers.Commands.CreateOffer
{
    public class CreateOfferCmdValidator : AbstractValidator<CreateOfferCmd>
    {
        public CreateOfferCmdValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.HotelId)
                .GreaterThan(0);
        }
    }
}
