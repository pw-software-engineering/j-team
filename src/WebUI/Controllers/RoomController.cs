using Application.Rooms;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Rooms.Commands.DeleteRoom;
using HotelReservationSystem.Application.Rooms.Commands.UpdateRoom;
using HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    // [Authorize]
    public class RoomController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<RoomDto>>> GetRoomsWithPagination([FromQuery] GetRoomsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateRoomCmd command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateRoomCmd command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRoomCmd { Id = id });

            return NoContent();
        }
    }
}
