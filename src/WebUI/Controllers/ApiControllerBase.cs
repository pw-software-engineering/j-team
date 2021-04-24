using System.Net;
using System.Threading.Tasks;
using Application.Auth;
using HotelReservationSystem.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservationSystem.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
        protected string GetHotelToken()
        {
            return Request.Headers["x-hotel-token"];
        }
        protected async Task<(int?, StatusCodeResult)> GetHotelIdFromToken()
        {
            var hotelId = await Mediator.Send(new GetHotelIdFromTokenQuery() { Token = GetHotelToken() });
            var statusCode = hotelId == null ? new StatusCodeResult((int)HttpStatusCode.Unauthorized)
            : new StatusCodeResult((int)HttpStatusCode.Accepted);
            return (hotelId, statusCode);
        }
    }
}
