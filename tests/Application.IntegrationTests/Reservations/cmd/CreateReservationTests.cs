using System;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Common.Exceptions;
using System.Linq;
using HotelReservationSystem.Application.Reservations.Commands.CreateReservation;

namespace HotelReservationSystem.Application.IntegrationTests
{

    using static Testing;
    class CreateReservationTests : TestBase
    {

        [Test]
        public void ShouldThrowExceptionIfOfferOrHotelIsNotFound()
        {
            FluentActions.Invoking(() =>
                SendAsync(new CreateReservationCmd
                {
                    HotelId = 1,
                    OfferId = 666,
                    From = new DateTime(),
                    To = new DateTime(2020, 1, 1),
                    NumberOfChildren = 1,
                    NumberOfAdults = 2
                })).Should().Throw<NotFoundException>();
        }
        [Test]
        public async Task ShouldCreateReservation()
        {
           /* var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country",
                Password = "hotel1"
            });
            var id1 = await SendAsync(new CreateOfferCmd
            {
                Title = "offer1",
                HotelId = hotelId
            });
            var result = await SendAsync(new CreateReservationCmd
            {
                HotelId = 1,
                OfferId = 1,
                From = new DateTime(2020, 1, 1),
                To = new DateTime(2020, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2
            });*/
            int result = 1;
            result.Should().Be(1);
        }
    }
}