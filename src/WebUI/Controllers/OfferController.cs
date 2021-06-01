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
using NSwag.Annotations;
using System.Collections.Generic;
using System.Net;
using WebUI;

namespace HotelReservationSystem.WebUI.Controllers
{
    [AuthorizeHotel]
    [OpenApiOperationProcessor(typeof(HotelHeaderOperationProcessor))]
    [Route("api-hotel/offers")]
    public class OfferController : ApiControllerBase
    {
        [HttpGet("{offerID}/rooms")]
        public async Task<ActionResult<List<RoomDto>>> rooms(int offerID, [FromQuery] RoomsQuery query)
        {
            query.OfferId = offerID;
            query.HotelId = await GetHotelIdFromToken();
            try
            {
                var response = await Mediator.Send(query);
                return response.Items;
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<OfferDto>>> GetOffersWithPagination([FromQuery] GetOffersWithPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return response.Items;
        }

        [HttpGet("{offerID}")]
        public async Task<ActionResult<OfferDto>> GetOffer(int offerID)
        {
            try
            {
                var hotelId = await GetHotelIdFromToken();
                var offer = await Mediator.Send(new GetOfferQuery { Id = offerID, HotelId = hotelId });
                return offer;
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateOfferResponse>> Create(CreateOfferCmd command)
        {
            var hotelId = await GetHotelIdFromToken();
            command.HotelId = hotelId;
            var response = new CreateOfferResponse();
            try
            {
                var id = await Mediator.Send(command);
                response.OfferID = id;
                return new ActionResult<CreateOfferResponse>(response);
            }
            catch (ValidationException ex)
            {
                response.Error = ex.Message;
                return new ApiResponse<CreateOfferResponse>(response);
            }
        }

        [HttpPatch("{offerID}")]
        public async Task<ActionResult> Update(int offerID, UpdateOfferCmd command)
        {
            command.Id = offerID;
            command.HotelId = await GetHotelIdFromToken();

            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
            catch (ValidationException ex)
            {
                return new ApiResponse(ex.Message, 400);
            }
        }

        [HttpDelete("{offerID}")]
        public async Task<ActionResult> Delete(int offerID)
        {
            try
            {
                var hotelId = await GetHotelIdFromToken();
                await Mediator.Send(new DeleteOfferCmd { Id = offerID, HotelId = hotelId });
                return Ok();
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
            catch (ValidationException ex)
            {
                var response = new DeleteOfferResponse() { Error = ex.Message };
                return new ApiResponse<DeleteOfferResponse>(response, 409);
            }
        }

        [HttpPost("{offerID}/rooms")]
        public async Task<ActionResult> AddRoom(int offerID, [FromBody] int roomID)
        {
            try
            {
                var hotelId = await GetHotelIdFromToken();
                await Mediator.Send(new AddOfferRoomCmd
                {
                    OfferId = offerID,
                    RoomId = roomID,
                    HotelId = hotelId
                });
                return Ok();
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
            catch (ValidationException ex)
            {
                return new ApiResponse(ex.Message, 400);
            }
        }

        [HttpDelete("{offerID}/rooms/{roomID}")]
        public async Task<ActionResult> DeleteRoom(int offerID, int roomID)
        {
            try
            {
                var hotelId = await GetHotelIdFromToken();
                await Mediator.Send(new DeleteOfferRoomCmd { OfferId = offerID, RoomId = roomID, HotelId = hotelId });
                return Ok();
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(401);
            }
        }
    }
}
