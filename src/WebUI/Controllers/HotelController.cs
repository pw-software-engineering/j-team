using Application.Hotels;
using Application.Offers;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Hotels.Commands.DeleteHotel;
using HotelReservationSystem.Application.Hotels.Commands.UpdateHotel;
using HotelReservationSystem.Application.Hotels.Queries.GetFilteredHotelOffers;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Offers.Commands.UpdateOffer;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelInfo;
using HotelReservationSystem.Application.Common.Exceptions;
using ValidationException = HotelReservationSystem.Application.Common.Exceptions.ValidationException;

namespace HotelReservationSystem.WebUI.Controllers
{
    [OpenApiOperationProcessor(typeof(ClientHeaderOperationProcessor))]
    [Route("api")]
    public class HotelController : ApiControllerBase
    {
        [HttpGet("hotels")]
        public async Task<ActionResult<List<HotelListedDto>>> GetHotelsWithPagination([FromQuery] GetHotelsWithPaginationQuery query)
        {
            var paginated = await Mediator.Send(query);
            return paginated.Items;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateHotelCmd command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("hotelInfo")]
        public async Task<ActionResult<HotelDto>> GetHotelInfo()
        {
            try
            {
                var query = new GetHotelInfoQuery();
                query.hotelId = await GetHotelIdFromToken();
                return await Mediator.Send(query);
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPatch("hotelInfo")]
        public async Task<ActionResult> Update(UpdateHotelCmd command)
        {
            command.Id = await GetHotelIdFromToken();

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteHotelCmd { Id = id });

            return NoContent();
        }

        [HttpGet("hotels/{id}/offers")]
        public async Task<ActionResult<List<OfferDto>>> GetFilteredHotelOffersWithPagination(int id, [FromQuery] GetFilteredHotelOffersQuery query)
        {
            query.HotelId = id;
            try
            {
                return await Mediator.Send(query);
            }
            catch (ValidationException)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
        }
    }
}
