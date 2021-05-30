using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Hotels.Queries.GetOfferInfo;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using FluentAssertions;
using HotelReservationSystem.Application.Common.Exceptions;


namespace HotelReservationSystem.Application.IntegrationTests.Hotels.Query
{
    using static Testing;
    class GetOfferInfoQueryTests
    {


        [Test]
        public async Task ShouldReturnNotFoundException()
        {
            var query = new GetOfferInfoQuery { hotelId = 1 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();

        }

        [Test]
        public async Task ShouldReturnDetailedOfferInfo()
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
                OfferTitle="SuperOferta",
                HotelId=hotelId,
                Description="Wakacje pod palmami"

            });

            var result = await SendAsync(new GetOfferInfoQuery
            {
                hotelId = hotelId,
                offerId =offerId
            });
            result.Should().NotBeNull();
            result.Title.Should().Be("SuperOferta");
            

        }
    }
}
