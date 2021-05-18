using FluentValidation;

namespace HotelReservationSystem.Application.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCmdValidator : AbstractValidator<UpdateOfferCmd>
    {
        public UpdateOfferCmdValidator()
        {
            RuleFor(v => v.OfferTitle)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
