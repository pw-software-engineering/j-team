using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Reservations.Queries.GetReservationsWithPagination;
using HotelReservationSystem.Application.Reservations.Commands.CreateReservation;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Linq;
using System;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;

using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;

namespace HotelReservationSystem.Application.IntegrationTests.Hotels.Query
{
    using static Testing;

    public class GetReservationsWithPaginationQueryTests : TestBase
    {
        [Test]
        public void ShouldRequireValidPaginationData()
        {
            var command = new GetReservationsWithPaginationQuery { PageNumber = -12, PageSize = 0 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnReservations()
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
                OfferTitle = "offer1",
                HotelId = hotelId,
                IsActive = true
            });
            var roomId = await SendAsync(new CreateRoomCmd
            {
                HotelID = hotelId,
                HotelRoomNumber = "123A"
            });

            await SendAsync(new AddOfferRoomCmd
            {
                OfferId = offerId,
                RoomId = roomId,
                HotelId = hotelId
            });

            var clientId = await SendAsync(new CreateClientCmd
            {
                Name = "John",
                Surname = "Kowalski",
                Username = "johnkowalski",
                Email = "john@kowalski.com"
            });

            var reservation = await SendAsync(new CreateReservationCmd
            {
                HotelId = hotelId,
                OfferId = offerId,
                ClientId = clientId,
                From = new DateTime(2020, 1, 1),
                To = new DateTime(2020, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2
            });
            var result = await SendAsync(new GetReservationsWithPaginationQuery
            {
                HotelId=hotelId
            });
            result.Should().NotBeNull();
            result.TotalPages.Should().Be(1);
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().Be(1);
            result.Items.Any(x => x.ReservationId== reservation ).Should().BeTrue();
        }
    }
}
