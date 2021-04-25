using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Common.Exceptions;
using System.Linq;
using HotelReservationSystem.Application.Rooms.Commands.DeleteRoom;
using HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination;
using System;

namespace HotelReservationSystem.Application.IntegrationTests
{

    using static Testing;
    class DeleteHotelRoomTests : TestBase
    {
        private HotelTestsFixture fixture;

        public DeleteHotelRoomTests()
        {
            this.fixture = new HotelTestsFixture(context);
        }
        [Test]
        public void ShouldThrowExceptionIfRoomIsNotFound()
        {
            FluentActions.Invoking(() =>
               SendAsync(new DeleteRoomCmd
               {
                   Id = 65,
                   HotelId = 123321
               })).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldNotDeleteRoomWithReservation()
        {
            var room = await fixture.CreateRoom(true, true, false);

            FluentActions.Invoking(() =>
             SendAsync(new DeleteRoomCmd
             {
                 Id = room.RoomId,
                 HotelId = room.HotelId
             })).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public async Task ShouldReplaceRoomInReservationWithOtherRooms()
        {
            var room = await fixture.CreateRoom(true, true, true);
            var reservations = context.Reservations.Where(x => x.RoomId == room.RoomId).ToList();
            reservations.All(x => x.RoomId == room.RoomId).Should().BeTrue();

            var result = await SendAsync(new DeleteRoomCmd
            {
                Id = room.RoomId,
                HotelId = room.HotelId
            });
            var rooms = await SendAsync(new GetRoomsWithPaginationQuery()
            {
                RoomNumber = room.HotelRoomNumber
            });
            rooms.Items.Any(x => x.RoomId == room.RoomId).Should().BeFalse();

            reservations = context.Reservations.Where(x => x.RoomId == room.RoomId).ToList();
            reservations.All(x => x.RoomId != room.RoomId).Should().BeTrue();
        }
        [Test]
        public async Task ShouldDeleteRoomWithoutReservations()
        {
            var room = await fixture.CreateRoom(true, false);

            var result = await SendAsync(new DeleteRoomCmd
            {
                Id = room.RoomId,
                HotelId = room.HotelId
            });
            var rooms = await SendAsync(new GetRoomsWithPaginationQuery()
            {
                RoomNumber = room.HotelRoomNumber
            });
            rooms.Items.Any(x => x.RoomId == room.RoomId).Should().BeFalse();
        }
    }
}
