using FluentValidation;
using HotelReservationSystem.Application.Reviews.Queries.GetReviewsWithPaginationQuery;

namespace HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination
{
    public class GetReviewsWithPaginationQueryValidator : AbstractValidator<GetReviewsWithPaginationQuery>
    {
        public GetReviewsWithPaginationQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
