using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class CreateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateHotelCmd();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateHotel()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateHotelCmd
            {
                Name = "Tasks"
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<Hotel>(itemId);

            item.Should().NotBeNull();
            item.Name.Should().Be(command.Name);
            item.CreatedBy.Should().Be(userId);
            item.Created.Should().BeCloseTo(DateTime.Now, 10000);
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }
    }
}
