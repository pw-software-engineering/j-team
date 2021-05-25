using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelReservationSystem.Application.Hotels.Queries.GetHotelInfo;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.IntegrationTests;
using FluentAssertions;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;

namespace HotelReservationSystem.Application.Hotels.Commands
{
    using static Testing;
    class GetHotelInfoQueryTestGetHotelInfoQueryTest : TestBase
    {


        [Test]
        public async Task ShouldReturnNotFoundException()
        {
            var query = new GetHotelInfoQuery { hotelId = 1 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();

        }

        [Test]
        public async Task ShouldReturnDetailedHotelInfo()
        {
            var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country",
                Password = "hotel1"
            });


            var result = await SendAsync(new GetHotelInfoQuery
            {
                hotelId = hotelId
            }) ;
            result.Should().NotBeNull();
            result.HotelName.Should().Be("hotel1");
            result.Country.Should().Be("country");
           
        }
    }
}
