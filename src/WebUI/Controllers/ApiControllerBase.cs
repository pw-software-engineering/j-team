using System;
using System.Net;
using System.Threading.Tasks;
using Application.Auth;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HotelReservationSystem.WebUI.Controllers
{
    [ApiController]
    [Route("api-hotel/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
        protected string GetHotelToken()
        {
            var rawString = Request.Headers["x-hotel-token"];
            var token = JsonConvert.DeserializeObject<HotelToken>(rawString);
            return token.Id.ToString();
        }
        protected async Task<int> GetHotelIdFromToken()
        {
            var hotelId = await Mediator.Send(new GetHotelIdFromTokenQuery() { Token = GetHotelToken() });
            return hotelId.Value;
        }
        protected async Task<int> GetClientIdFromToken()
        {
            var clientId = await Mediator.Send(new GetClientIdFromTokenQuery() { Token = "" });
            return clientId;
        }
    }

}
