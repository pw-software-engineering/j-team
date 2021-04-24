using Application.Offers;
using Application.Rooms;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Offers.Commands.UpdateOffer;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using HotelReservationSystem.Application.Offers.Queries.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    // [Authorize]
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

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateOfferCmd command)
        {
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
            catch(NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
