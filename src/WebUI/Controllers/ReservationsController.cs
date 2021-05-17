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
using NSwag.Annotations;

namespace HotelReservationSystem.WebUI.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    [AuthorizeHotel]
    [OpenApiOperationProcessor(typeof(HotelHeaderOperationProcessor))]
    public class ReservationsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ReservationDto>>> GetReservationsWithPagination(int page_num, int page_siz, int? roomID, bool? currentOnly)
        {
            var hotelId =  await GetHotelIdFromToken();
            var result = await Mediator.Send(new GetReservationsWithPaginationQuery {PageNumber=page_num, PageSize=page_siz,
                HotelId=hotelId,RoomID=roomID,CurrentOnly=currentOnly});
            if (roomID==null && !result.Items.Any())
            {
                return NotFound();
            }
            foreach(var item in result.Items)
            {
                Console.WriteLine(item.AdultsCount + " " + item.RoomId + " " + item.Surname);
            }
            return result;
        }

       

        
    }
}
