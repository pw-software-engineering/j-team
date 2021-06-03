﻿using System;
using System.Threading.Tasks;
using Application.Auth;
using HotelReservationSystem.Application.Clients.Commands.GetClientToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HotelReservationSystem.Application.Common.Security
{
    /// <summary>
    /// Specifies the class this attribute is applied to requires authorization.
    /// </summary>
    public class AuthorizeHotelAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
        /// </summary>
        public AuthorizeHotelAttribute() : base(typeof(HotelTokenFilter))
        { }
    }
    public class HotelTokenFilter : IAsyncAuthorizationFilter
    {
        public HotelTokenFilter()
        {
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var rawToken = context.HttpContext.Request.Headers["x-hotel-token"];
            var token = JsonConvert.DeserializeObject<HotelToken>(rawToken);
            var mediator = context.HttpContext.RequestServices.GetService<ISender>();
            var hotelId = await mediator.Send(new GetHotelIdFromTokenQuery() { Token = token.Id.ToString() });
            if (hotelId is null)
            {
                var result = new ObjectResult(new AuthFailedResult() { Desc = "Authentication failed" });
                result.StatusCode = 401;
                context.Result = result;
            }
        }
    }
    public class AuthorizeClientAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
        /// </summary>
        public AuthorizeClientAttribute() : base(typeof(ClientTokenFilter))
        { }
    }
    public class AuthFailedResult
    {
        public string Desc { get; set; }
    }
    public class HotelToken
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class ClientTokenFilter : IAsyncAuthorizationFilter
    {
        public ClientTokenFilter()
        {
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var rawToken = context.HttpContext.Request.Headers["x-client-token"];
            var mediator = context.HttpContext.RequestServices.GetService<ISender>();
            var clientId = await mediator.Send(new GetClientIdFromTokenQuery() { Token = rawToken });
            if (clientId is null)
            {
                var result = new ObjectResult(new AuthFailedResult() { Desc = "Authentication failed" });
                result.StatusCode = 401;
                context.Result = result;
            }
        }
    }
}
