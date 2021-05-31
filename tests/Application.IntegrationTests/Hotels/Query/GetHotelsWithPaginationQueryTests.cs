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
using HotelReservationSystem.Application.Hotels.Queries.GetHotelsWithPagination;

namespace HotelReservationSystem.Application.Hotels.Queries
{
    using static Testing;
    class GetHotelsWithPaginationQueryTests : TestBase
    {


        [Test]
        public void ShouldRequireValidPaginationData()
        {
            var command = new GetHotelsWithPaginationQuery { PageNumber = -12, PageSize = 0 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(null, null, null)]
        [TestCase("new", null, null)]
        [TestCase(null, "new city", "new hotel")]
        public async Task ShouldReturnHotels(string country, string city, string hotelName)
        {
            var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country",
                Password = "hotel1"
            });
            var hotel2Id = await SendAsync(new CreateHotelCmd
            {
                Name = "new hotel",
                City = "new city",
                Country = "new country",
                Password = "hotel1"
            });

            var result = await SendAsync(new GetHotelsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 1,
                HotelName = hotelName,
                City = city,
                Country = country
            });
            result.Should().NotBeNull();
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().Be(1);
            result.Items.Any(x => x.HotelID == hotelId || x.HotelID == hotel2Id).Should().BeTrue();
            result.Items.All(x =>
            {
                return (hotelName is null || x.HotelName.StartsWith(hotelName))
                && (country is null || x.Country.StartsWith(country))
                && (city is null || x.City.StartsWith(city));
            }).Should().BeTrue();
        }
    }
}
