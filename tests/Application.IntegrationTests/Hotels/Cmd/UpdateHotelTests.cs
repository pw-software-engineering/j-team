using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Hotels.Commands.UpdateHotel;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain.Enums;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace HotelReservationSystem.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class UpdateTodoItemDetailTests : TestBase
    {
        [Test]
        public void ShouldRequireValidHotelId()
        {
            var command = new UpdateHotelCmd
            {
                Id = 99,
                Name = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateHotel()
        {
            var userId = await RunAsDefaultUserAsync();

            var itemId = await SendAsync(new CreateHotelCmd
            {
                Name = "New Item"
            });

            var item = await FindAsync<Hotel>(itemId);

            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
