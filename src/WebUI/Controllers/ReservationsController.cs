using Application.Auth;
using Application.Reservations;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Rooms.Queries.GetReservationsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    [AuthorizeHotel]
    public class ReservationsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ReservationDto>>> GetReservationsWithPagination([FromQuery] GetReservationsWithPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            if (query.RoomID==null && !result.Items.Any())
            {
                return NotFound();
            }
            return result;
        }

       

        
    }
}
