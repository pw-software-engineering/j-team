using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Offers.Queries.Rooms;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Common.Exceptions;
using System.Linq;

namespace HotelReservationSystem.Application.IntegrationTests
{

    using static Testing;
    class RoomsTests : TestBase
    {

        [Test]
        public void ShouldThrowExceptionIfOfferIsNotFound()
        {
            FluentActions.Invoking(() =>
               SendAsync(new RoomsQuery
               {
                   PageNumber = 1,
                   PageSize = 1,
                   OfferId = 65
               })).Should().Throw<NotFoundException>();
        }
        [Test]
        public async Task ListsRoomsForOfferWithOneRoom()
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
                HotelId = hotelId
            });
            var roomId = await SendAsync(new CreateRoomCmd
            {
                HotelRoomNumber = "509A",
                HotelID = hotelId,
                OfferID = offerId
            });
            var result = await SendAsync(new RoomsQuery
            {
                PageNumber = 1,
                PageSize = 1,
                OfferId = offerId,
                HotelId = hotelId
            });

            result.Items.Any(x => x.RoomId == roomId).Should().BeTrue();
        }
    }
}