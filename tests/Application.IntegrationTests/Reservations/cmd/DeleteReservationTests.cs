using System;
using System.Threading.Tasks;
using FluentAssertions;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Reservations.Commands.CreateReservation;
using HotelReservationSystem.Application.Reservations.Commands.DeleteReservation;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using NUnit.Framework;

namespace HotelReservationSystem.Application.IntegrationTests
{

    using static Testing;
    class DeleteReservationTests : TestBase
    {
        [Test]
        public void ShouldThrowExceptionIfClientOrReservationIsNotFound()
        {
            FluentActions.Invoking(() =>
                SendAsync(new DeleteReservationCmd
                {
                    ReservationId = 1,
                    ClientId = -1
                })).Should().Throw<NotFoundException>();
        }
        [Test]
        public async Task ShouldDeleteReservation()
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
                Email = "john@kowalski.com",
                Password = "pass"
            });
            var reservationId = await SendAsync(new CreateReservationCmd
            {
                HotelId = hotelId,
                OfferId = offerId,
                ClientId = clientId,
                From = new DateTime(2022, 1, 1),
                To = new DateTime(2022, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2,
                
            });
            var result = await SendAsync(new DeleteReservationCmd
            {
                ReservationId = reservationId,
                ClientId = clientId
            });
            result.Should().BePositive();
        }
        [Test]
        public async Task ShouldNotDeleteReservationFromThePast()
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
                Email = "john@kowalski.com",
                Password = "pass"
            });
            var reservationId = await SendAsync(new CreateReservationCmd
            {
                HotelId = hotelId,
                OfferId = offerId,
                ClientId = clientId,
                From = new DateTime(2020, 1, 1),
                To = new DateTime(2020, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2,
                
            });
            FluentActions.Invoking(() =>
                SendAsync(new DeleteReservationCmd
                {
                    ReservationId = reservationId,
                    ClientId = clientId
                })).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldDeleteReservations()
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
            var roomId2 = await SendAsync(new CreateRoomCmd
            {
                HotelID = hotelId,
                HotelRoomNumber = "123A2"
            });
            await SendAsync(new AddOfferRoomCmd
            {
                OfferId = offerId,
                RoomId = roomId,
                HotelId = hotelId
            });
            await SendAsync(new AddOfferRoomCmd
            {
                OfferId = offerId,
                RoomId = roomId2,
                HotelId = hotelId
            });
            var clientId = await SendAsync(new CreateClientCmd
            {
                Name = "John",
                Surname = "Kowalski",
                Username = "johnkowalski",
                Email = "john@kowalski.com",
                Password = "pass"
            });
            var reservationId = await SendAsync(new CreateReservationCmd
            {
                HotelId = hotelId,
                OfferId = offerId,
                ClientId = clientId,
                From = new DateTime(2022, 1, 1),
                To = new DateTime(2022, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2,
                
            });
            var reservationId2 = await SendAsync(new CreateReservationCmd
            {
                HotelId = hotelId,
                OfferId = offerId,
                ClientId = clientId,
                From = new DateTime(2022, 1, 1),
                To = new DateTime(2022, 1, 3),
                NumberOfChildren = 1,
                NumberOfAdults = 2,
                
            });
            var result = await SendAsync(new DeleteReservationCmd
            {
                ReservationId = reservationId,
                ClientId = clientId
            });
            FluentActions.Invoking(() =>
                SendAsync(new DeleteReservationCmd
                {
                    ReservationId = reservationId,
                    ClientId = clientId
                })).Should().Throw<NotFoundException>();
            var result2 = await SendAsync(new DeleteReservationCmd
            {
                ReservationId = reservationId2,
                ClientId = clientId
            });
            result.Should().Be(reservationId);
            result2.Should().Be(reservationId2);
        }
    }
}