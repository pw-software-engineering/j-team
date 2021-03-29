using FluentValidation;

namespace HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination
{
    public class GetOffersWithPaginationQueryValidator : AbstractValidator<GetOffersWithPaginationQuery>
    {
        public GetOffersWithPaginationQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
