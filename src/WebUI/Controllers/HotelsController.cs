using Application.Hotels;
using Application.Offers;
using HotelReservationSystem.Application.Hotels.Queries.GetFilteredHotelOffers;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination;
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
    [Route("api-client")]
    public class HotelsController : ApiControllerBase
    {
        [HttpGet("hotels")]
        public async Task<ActionResult<List<HotelListedDto>>> GetHotelsWithPagination([FromQuery] GetHotelsWithPaginationQuery query)
        {
            var paginated = await Mediator.Send(query);
            return paginated.Items;
        }



        [HttpGet("hotels/{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelInfo(int id)
        {
            Console.WriteLine("hej");
            try
            {
                var query = new GetHotelInfoQuery();
                query.hotelId = id;
                Console.WriteLine(query.hotelId);
                return await Mediator.Send(query);
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
    }
}
