using System;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Reservations.Commands.CreateReservation;
using HotelReservationSystem.Application.Reservations.Commands.DeleteReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.WebUI.Controllers
{
    public class ReservationController : ApiControllerBase
    {
        [HttpPost("/api/hotels/{hotelID}/offers/{offerID}/reservations")]
        public async Task<ActionResult<int>> Create(int hotelID, int offerID, CreateReservationCmd command)
        {
            Console.WriteLine("----HotelID: {0}", hotelID);
            command.HotelId = hotelID;
            command.OfferId = offerID;
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
        [HttpDelete("/{reservationID}")]
        public async Task<ActionResult<int>> Delete(int reservationID)
        {
            DeleteReservationCmd command = new DeleteReservationCmd
            {
                ReservationId = reservationID,
                ClientId = 1
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