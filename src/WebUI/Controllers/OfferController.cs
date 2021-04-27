using Application.Offers;
using Application.Rooms;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Offers.Commands.UpdateOffer;
using HotelReservationSystem.Application.Offers.Queries;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using HotelReservationSystem.Application.Offers.Queries.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    [AuthorizeHotel]
    public class OfferController : ApiControllerBase
    {
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<PaginatedList<RoomDto>>> rooms(int id, [FromQuery] RoomsQuery query)
        {
            query.OfferId = id;

            return await Mediator.Send(query);
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedList<OfferDto>>> GetOffersWithPagination([FromQuery] GetOffersWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDto>> GetOffer(int id)
        {
            return await Mediator.Send(new GetOfferQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateOfferCmd command)
        {
            var hotelId = await GetHotelIdFromToken();
            command.HotelId = hotelId;
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateOfferCmd command)
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
                await Mediator.Send(new DeleteOfferCmd { Id = id });
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{offerId}/rooms")]
        public async Task<ActionResult> AddRoom(int offerId, int roomId)
        {
            await Mediator.Send(new AddOfferRoomCmd { OfferId = offerId, RoomId = roomId });

            return Ok();
        }

        [HttpDelete("{offerId}/rooms/{roomId}")]
        public async Task<ActionResult> DeleteRoom(int offerId, int roomId)
        {
            await Mediator.Send(new DeleteOfferRoomCmd { OfferId = offerId, RoomId = roomId });

            return Ok();
        }
    }
}
