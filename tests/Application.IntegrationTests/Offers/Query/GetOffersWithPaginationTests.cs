using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Hotels.Commands.DeleteHotel;
using HotelReservationSystem.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using System.Linq;

namespace HotelReservationSystem.Application.IntegrationTests.Hotels.Commands
{
    using static Testing;

    public class GetOffersWithPaginationQueryTests : TestBase
    {
        [Test]
        public void ShouldRequireValidPaginationData()
        {
            var command = new GetOffersWithPaginationQuery { PageNumber = -12, PageSize = 0 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnOffers()
        {
            var hotelId = await SendAsync(new CreateHotelCmd
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
            var id2 = await SendAsync(new CreateOfferCmd
            {
                Title = "offer2",
                HotelId = hotelId
            });

            var result = await SendAsync(new GetOffersWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 1
            });
            result.Should().NotBeNull();
            result.TotalPages.Should().Be(2);
            result.TotalCount.Should().Be(2);
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().Be(1);
            result.Items.Any(x => x.OfferId == id1 || x.OfferId == id2).Should().BeTrue();
        }
    }
}
