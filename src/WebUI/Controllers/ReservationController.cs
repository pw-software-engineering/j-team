using System;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Reservations.Commands.CreateReservation;
using HotelReservationSystem.Application.Reservations.Commands.DeleteReservation;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace HotelReservationSystem.WebUI.Controllers
{
    [AuthorizeClient]
    [OpenApiOperationProcessor(typeof(ClientHeaderOperationProcessor))]
    //[Route("api-hotel")]
    public class ReservationController : ApiControllerBase
    {
        [HttpPost("/api-client/hotels/{hotelID}/offers/{offerID}/reservations")]
        public async Task<ActionResult<int>> Create(int hotelID, int offerID, CreateReservationCmd command)
        {
            command.HotelId = hotelID;
            command.OfferId = offerID;
            command.ClientId = await GetClientId();
            Console.WriteLine("~~~ {0}", command.ClientId);
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("Resource not found: e.g. there is no hotel with ID equal to hotelID " +
                                "that has an offer with ID equal to offerID or hotel/offer does not exist");
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }
        [HttpDelete("/api-client/reservations/{reservationID}")]
        public async Task<ActionResult<int>> Delete(int reservationID)
        {
            DeleteReservationCmd command = new DeleteReservationCmd
            {
                ReservationId = reservationID,
                ClientId = await GetClientId()
            };
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ForbiddenAccessException)
            {
                return Unauthorized();
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }
    }
}