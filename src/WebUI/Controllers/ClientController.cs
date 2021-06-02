using System.Threading.Tasks;
using Application.Auth;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.WebUI.Controllers
{
    [Route("api-client")]
    public class ClientController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateClientCmd command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginCmd command)
        {
            try
            {
                var token = await Mediator.Send(command);
                return new ActionResult<string>(token);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
        }
    }
}