using System.Security.Authentication;
using System.Threading.Tasks;
using Application.Auth;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Exceptions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.WebUI.Controllers
{
    public class ClientController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateClientCmd command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<ActionResult<ClientSessionToken>> Login(LoginCmd command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
        }
    }
}