using HotelReservationSystem.Application.Offers.Commands.DeleteOffer;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Offers.Queries.GetOffersWithPagination;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using HotelReservationSystem.Application.Common.Exceptions;
using System.Linq;

namespace HotelReservationSystem.Application.IntegrationTests
{
    
    using static Testing;
    class DeleteOfferTests :TestBase
    {

        [Test]
        public void ShouldThrowExceptionIfOfferIsNotFound()
        {
            FluentActions.Invoking(() =>
               SendAsync(new DeleteOfferCmd
            {
                Id = 65
            })).Should().Throw<NotFoundException>();
        }
        [Test]
        public async Task DeletedOfferIsNotVisibleOnOffersLists()
        {
            var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country"
            });
            var id1 = await SendAsync(new CreateOfferCmd
            {
                Title = "offer1",
                HotelId = hotelId
            });
         
           await SendAsync(new DeleteOfferCmd
             {
                   Id = id1
              });
            var result = await SendAsync(new GetOffersWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 1
            });
            result.Items.Any(x => x.OfferId == id1 ).Should().BeFalse();
        }
    }
}
