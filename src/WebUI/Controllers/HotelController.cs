using Application.Hotels;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Hotels.Commands.DeleteHotel;
using HotelReservationSystem.Application.Hotels.Commands.UpdateHotel;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelInfo;
using HotelReservationSystem.Application.Common.Exceptions;

using ValidationException = HotelReservationSystem.Application.Common.Exceptions.ValidationException;
using HotelReservationSystem.Application.Hotels;
using HotelReservationSystem.Application.Hotels.Queries.GetOfferInfo;
using HotelReservationSystem.Application.Common.Security;

namespace HotelReservationSystem.WebUI.Controllers
{
    [OpenApiOperationProcessor(typeof(HotelHeaderOperationProcessor))]
    [AuthorizeHotel]
    [Route("api-hotel")]
    public class HotelController : ApiControllerBase
    {

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

    }
}
