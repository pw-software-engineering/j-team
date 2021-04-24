using Application.Rooms;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Rooms.Commands.DeleteRoom;
using HotelReservationSystem.Application.Rooms.Commands.UpdateRoom;
using HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<RoomDto>>> GetRoomsWithPagination([FromQuery] GetRoomsWithPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            if (!string.IsNullOrEmpty(query.RoomNo) && !result.Items.Any())
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateRoomCmd command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (ValidationException)
            {
                return new StatusCodeResult((int)HttpStatusCode.Conflict);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
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
            try
            {
                var result = await Mediator.Send(new DeleteRoomCmd { Id = id });
                return Ok();
            }
            catch (ValidationException)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
        }
    }
}
