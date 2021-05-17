using System.Threading.Tasks;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
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
    }
}