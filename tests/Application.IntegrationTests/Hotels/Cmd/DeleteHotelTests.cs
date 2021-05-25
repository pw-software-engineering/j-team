using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Hotels.Commands.DeleteHotel;
using HotelReservationSystem.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HotelReservationSystem.Application.IntegrationTests.Hotels.Commands
{
    using static Testing;

    public class DeleteHotelTests : TestBase
    {
        // [Test]
        // public void ShouldRequireValidHotelId()
        // {
        //     var command = new DeleteHotelCmd { Id = 99 };

        //     FluentActions.Invoking(() =>
        //         SendAsync(command)).Should().Throw<NotFoundException>();
        // }

        // [Test]
        // public async Task ShouldDeleteHotel()
        // {
        //     var itemId = await SendAsync(new CreateHotelCmd
        //     {
        //         Name = "New Item"
        //     });

        //     await SendAsync(new DeleteHotelCmd
        //     {
        //         Id = itemId
        //     });
        // }
    }
}
