
using Application.Auth;
using Application.Reservations;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Common.Models;
using HotelReservationSystem.Application.Common.Security;
using HotelReservationSystem.Application.Reservations.Queries.GetReservationsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NSwag.Annotations;
using System.Collections.Generic;

namespace HotelReservationSystem.WebUI.Controllers
{
    //[ApiController]
    [AuthorizeHotel]
    [OpenApiOperationProcessor(typeof(HotelHeaderOperationProcessor))]
    [Route("api-hotel/reservations")]
    public class ReservationsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetReservationsWithPagination(int page_num, int page_siz, int? roomID, bool? currentOnly)
        {
            var hotelId =  await GetHotelIdFromToken();
            try
            {
                var response = await Mediator.Send(new GetReservationsWithPaginationQuery
                {
                    PageNumber = page_num,
                    PageSize = page_siz,
                    HotelId = hotelId,
                    RoomID = roomID,
                    CurrentOnly = currentOnly
                });
                if (roomID == null && !response.Items.Any())
                {
                    return new StatusCodeResult(404);
                }
                return response.Items;
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (ForbiddenAccessException)
            {
                return new StatusCodeResult(403);
            }
            
        }
    }
}
