using MediatR;

namespace Application.Common.Models
{
    public class PageableQuery<T> : IRequest<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}