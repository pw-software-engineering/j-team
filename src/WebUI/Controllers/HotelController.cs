﻿using Application.Hotels;
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
using System.Threading.Tasks;

namespace HotelReservationSystem.WebUI.Controllers
{
    [OpenApiOperationProcessor(typeof(ClientHeaderOperationProcessor))]
    public class HotelController : ApiControllerBase
    {
        [HttpGet]
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateHotelCmd command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteHotelCmd { Id = id });

            return NoContent();
        }

        [HttpGet("{id}/offers")]
        public async Task<ActionResult<List<OfferDto>>> GetFilteredHotelOffersWithPagination(int id, [FromQuery] GetFilteredHotelOffersQuery query)
        {
            query.HotelId = id;
            return await Mediator.Send(query);
        }
    }
}
