﻿using Application.Offers;
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
using Application.Reviews;
using HotelReservationSystem.Application.Reviews.Queries.GetReviewsWithPaginationQuery;
using HotelReservationSystem.Application.Reviews.Cmd.CreateRevewCmd;
using Application.Reviews.Commands;

namespace HotelReservationSystem.WebUI.Controllers
{
    [AuthorizeHotel]
    [OpenApiOperationProcessor(typeof(HotelHeaderOperationProcessor))]
    [Route("client-api/hotels")]
    public class ReviewsController : ApiControllerBase
    {
        [HttpGet("{hotelID}/offers/{offerID}/reviews")]
        public async Task<ActionResult<List<ReviewDto>>> GetOffersWithPagination([FromQuery] GetReviewsWithPaginationQuery query)
        {
            try
            {
                var response = await Mediator.Send(query);
                return response.Items;
            }
            catch (NotFoundException)
            {
                return new StatusCodeResult(404);
            }
        }
        [HttpPost("{hotelID}/offers/{offerID}/reviews")]
        public async Task<ApiResponse<CreateReviewResponse>> CreateReview([FromQuery] int hotelID, [FromQuery] int offerID, CreateReviewCmd cmd)
        {
            var response = new CreateReviewResponse();
            try
            {
                cmd.HotelId = hotelID;
                cmd.OfferId = offerID;
                cmd.ClientId = await GetClientIdFromToken();
                var id = await Mediator.Send(cmd);
                response.ReviewID = id;
                return new ApiResponse<CreateReviewResponse>(response, 200);
            }
            catch (NotFoundException)
            {
                response.Error = "not found";
                return new ApiResponse<CreateReviewResponse>(response, 404);
            }
            catch (ValidationException)
            {
                response.Error = "Validation error";
                return new ApiResponse<CreateReviewResponse>(response, 400);
            }
        }
        [HttpPut("{hotelID}/offers/{offerID}/reviews")]
        public async Task<ActionResult> UpdateReview([FromQuery] int hotelID, [FromQuery] int offerID, UpdateReviewCmd cmd)
        {
            try
            {
                cmd.ClientId = await GetClientIdFromToken();
                cmd.HotelId = hotelID;
                cmd.OfferId = offerID;
                var id = await Mediator.Send(cmd);
                return new StatusCodeResult(200);
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
        [HttpDelete("{hotelID}/offers/{offerID}/reviews/{reviewID}")]
        public async Task<ActionResult> DeleteReview([FromQuery] int hotelID, [FromQuery] int offerID, [FromQuery] int reviewID)
        {
            var cmd = new DeleteReviewCmd()
            {
                OfferId = offerID,
                HotelId = hotelID,
                ReviewId = reviewID,
                ClientId = await GetClientIdFromToken()
            };
            try
            {
                await Mediator.Send(cmd);
                return new StatusCodeResult(200);
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