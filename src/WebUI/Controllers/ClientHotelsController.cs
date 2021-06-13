using Application.Hotels;
using Application.Offers;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Hotels;
using HotelReservationSystem.Application.Hotels.Queries;
using HotelReservationSystem.Application.Hotels.Queries.GetFilteredHotelOffers;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination;
using HotelReservationSystem.Application.Hotels.Queries.GetOfferInfo;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelInfo;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ValidationException = HotelReservationSystem.Application.Common.Exceptions.ValidationException;
using HotelReservationSystem.Application.Common.Exceptions;
using System;

namespace HotelReservationSystem.WebUI.Controllers
{
    [OpenApiOperationProcessor(typeof(ClientHeaderOperationProcessor))]
    [AuthorizeClient]
    [Route("api-client")]
    public class HotelsController : ApiControllerBase
    {
        [HttpGet("hotels")]
        public async Task<ActionResult<List<HotelListedDto>>> GetHotelsWithPagination([FromQuery] GetHotelsWithPaginationQuery query)
        {
            var paginated = await Mediator.Send(query);
            return paginated.Items;
        }
        [HttpGet("hotels/{hotelId}")]
        public async Task<ActionResult<HotelDetailsDto>> GetHotel(int hotelId)
        {
            try
            {
                var response = await Mediator.Send(new GetHotelQuery() { HotelId = hotelId });
                return response;
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
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
        [HttpGet("hotels/{id}/offers/{offerId}")]
        public async Task<ActionResult<DetailedOfferDto>> GetOfferInfo(int id, int offerId)
        {
            try
            {
                var query = new GetOfferInfoQuery();
                query.hotelId = id;
                query.offerId = offerId;
                return await Mediator.Send(query);
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
        }
    }
}
