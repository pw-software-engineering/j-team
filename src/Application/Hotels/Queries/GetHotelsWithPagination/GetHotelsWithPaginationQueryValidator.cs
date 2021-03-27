﻿using FluentValidation;

namespace HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination
{
    public class GetHotelsWithPaginationQueryValidator : AbstractValidator<GetHotelsWithPaginationQuery>
    {
        public GetHotelsWithPaginationQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
