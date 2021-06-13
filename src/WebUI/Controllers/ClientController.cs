using System.Threading.Tasks;
using Application.Auth;
using Application.Clients;
using Application.Clients.Commands;
using HotelReservationSystem.Application.Clients;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Security;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.WebUI.Controllers
{
    [Route("api-client/client")]
    public class ClientController : ApiControllerBase
    {
        [HttpGet]
        [AuthorizeClient]
        public async Task<ClientDto> Get()
        {
            var id = await GetClientIdFromToken();
            return await Mediator.Send(new GetClientInfoQuery() { ClientId = id });
        }
        [HttpPatch]
        [AuthorizeClient]
        public async Task<ActionResult> Patch(UpdateClientCmd cmd)
        {
            var id = await GetClientIdFromToken();
            cmd.Id = id;
            try
            {
                await Mediator.Send(cmd);
                return new StatusCodeResult(200);
            }
            catch (ValidationException)
            {
                return new StatusCodeResult(400);
            }
        }
        [HttpPost]
        [AuthorizeClient]
        public async Task<ActionResult<int>> Create(CreateClientCmd command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("login")]
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