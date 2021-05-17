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
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Reservations.Commands.CreateReservation;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;

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
            var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country",
                Password = "hotel1"
            });
            var offerId = await SendAsync(new CreateOfferCmd
            {
                Title = "offer1",
                HotelId = hotelId,
                IsActive = true
            });
            var roomId = await SendAsync(new CreateRoomCmd
            {
                HotelID = hotelId,
                OfferID = offerId,
                HotelRoomNumber = "123A"
            });
            var clientId = await SendAsync(new CreateClientCmd
            {
                Name = "John",
                Surname = "Kowalski",
                Username = "johnkowalski",
                Email = "john@kowalski.com"
            });
            var result = await SendAsync(new CreateReservationCmd
            {
                HotelId = hotelId,
                OfferId = offerId,
                ClientId = clientId,
                From = new DateTime(2020, 1, 1),
                To = new DateTime(2020, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2
            });
           result.Should().BePositive();
        }
    }
}