using Application.Offers;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Offers.Commands.UpdateOffer;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    // [Authorize]
    public class OfferController : ApiControllerBase
    {
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
            await Mediator.Send(new DeleteOfferCmd { Id = id });

            return NoContent();
        }
    }
}
