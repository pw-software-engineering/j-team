using Application.Common.Models;
using Application.Offers;
using Application.Reviews;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Reviews.Queries
{
    public class GetReviewQuery : IRequest<ReviewDto>
    {
        public int ReviewID { get; set; }
    }

    public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, ReviewDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {

            var review = await _context.Reviews.FindAsync(request.ReviewID);
            return _mapper.Map<ReviewDto>(review);
        }
    }
}
