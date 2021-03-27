using Application.Hotels;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Hotels.Commands.CreateTodoItem;
using HotelReservationSystem.Application.Hotels.Commands.DeleteTodoItem;
using HotelReservationSystem.Application.Hotels.Commands.UpdateTodoItem;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    [Authorize]
    public class HotelController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<HotelDto>>> GetHotelsWithPagination([FromQuery] GetHotelsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateHotelCmd command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateHotelCmd command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteHotelCmd { Id = id });

            return NoContent();
        }
    }
}
