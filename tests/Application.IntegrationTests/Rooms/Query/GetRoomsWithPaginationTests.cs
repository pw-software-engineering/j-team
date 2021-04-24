using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Hotels.Commands.DeleteHotel;
using HotelReservationSystem.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using System.Linq;

namespace HotelReservationSystem.Application.IntegrationTests.Hotels.Commands
{
    using static Testing;

    public class GetRoomsWithPaginationQueryTests : TestBase
    {
        [Test]
        public void ShouldRequireValidPaginationData()
        {
            var command = new GetRoomsWithPaginationQuery { PageNumber = -12, PageSize = 0 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnRooms()
        {
            var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country"
            });
            var offerid = await SendAsync(new CreateOfferCmd
            {
                OfferTitle = "offer1",
                HotelId = hotelId
            });
            var room1id = await SendAsync(new CreateRoomCmd
            {
                OfferID = offerid,
                HotelID = hotelId,
                HotelRoomNumber="420"
            });
            var room2id = await SendAsync(new CreateRoomCmd
            {
                OfferID = offerid,
                HotelID = hotelId,
                HotelRoomNumber = "489"
            });
            var result = await SendAsync(new GetRoomsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 1
            });
            result.Should().NotBeNull();
            result.TotalPages.Should().Be(2);
            result.TotalCount.Should().Be(2);
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().Be(1);
            result.Items.Any(x => x.RoomId == room1id || x.RoomId == room2id).Should().BeTrue();
        }
    }
}
