using System.Threading.Tasks;
using Application.Auth;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Security;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.WebUI.Controllers
{
    [Route("api-client/client")]
    public class ClientController : ApiControllerBase
    {
        [HttpPost]
        [AuthorizeClient]
        public async Task<ActionResult<int>> Create(CreateClientCmd command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("client/login")]
        public async Task<ActionResult<ClientToken>> Login(ClientLoginCmd command)
        {
            try
            {
                var token = await Mediator.Send(command);
                return token;
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
        }
    }
}