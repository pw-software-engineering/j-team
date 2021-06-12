using Application.Auth;
using Application.Rooms;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Rooms.Commands.DeleteRoom;
using HotelReservationSystem.Application.Rooms.Commands.UpdateRoom;
using HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NSwag.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebUI;

namespace HotelReservationSystem.WebUI.Controllers
{
    [AuthorizeHotel]
    [OpenApiOperationProcessor(typeof(HotelHeaderOperationProcessor))]
    [Route("api-hotel/rooms")]
    public class RoomController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> GetRoomsWithPagination([FromQuery] GetRoomsWithPaginationQuery query)
        {
            query.HotelId = await GetHotelIdFromToken();
            var result = await Mediator.Send(query);
            if (!string.IsNullOrEmpty(query.RoomNumber) && !result.Items.Any())
            {
                return new StatusCodeResult(404);
            }
            return result.Items;
        }

        [HttpPost]
        public async Task<ActionResult<CreateRoomResponse>> Create([FromBody] string hotel_room_number)
        {

            try
            {
                var hotelId = await GetHotelIdFromToken();
                var response = new CreateRoomResponse();
                var id = await Mediator.Send(new CreateRoomCmd
                {
                    HotelRoomNumber = hotel_room_number,
                    HotelID = hotelId
                });
                response.RoomID = id;
                return new ActionResult<CreateRoomResponse>(response);
            }

            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
            catch (ValidationException ex)
            {
                return new ApiResponse(ex.Message, 409);
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
            var hotelId = await GetHotelIdFromToken();
            try
            {
                var result = await Mediator.Send(new DeleteRoomCmd { Id = id, HotelId = hotelId });
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                var response = new DeleteRoomResponse() { Error = ex.Message };
                return new ApiResponse<DeleteRoomResponse>(response, 409);
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
        }
    }
}
