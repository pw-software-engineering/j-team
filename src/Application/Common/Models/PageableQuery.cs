using MediatR;

namespace Application.Common.Models
{
    public class PageableQuery<T> : IRequest<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}