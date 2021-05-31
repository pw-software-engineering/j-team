using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Reviews.Commands;
using FluentAssertions;
using HotelReservationSystem.Application.Clients.Commands.CreateClient;
using HotelReservationSystem.Application.Common.Exceptions;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.IntegrationTests;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using HotelReservationSystem.Application.Reviews.Cmd.CreateRevewCmd;
using HotelReservationSystem.Application.Reviews.Cmd.UpdateRevewCmd;
using HotelReservationSystem.Application.Reviews.Queries.GetReviewsWithPaginationQuery;
using NUnit.Framework;


namespace HotelReservationSystem.Application.IntegrationTests.Reviews
{
    using static Testing;
    public class ReviewsCRUDTests : TestBase
    {
        private async Task<(int offerId, int hotelId, int clientId)> CreateHotelAndOffer()
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

            var clientId = await SendAsync(new CreateClientCmd
            {
                Name = "John",
                Surname = "Kowalski",
                Username = "johnkowalski",
                Email = "john@kowalski.com"
            });
            return (offerId, hotelId, clientId);
        }
        public async Task<(int offerId, int hotelId, int clientId, int reviewID)> CreateReview()
        {
            (int offerId, int hotelId, int clientId) = await CreateHotelAndOffer();
            var createCmd = new CreateReviewCmd()
            {
                HotelID = hotelId,
                OfferID = offerId,
                ClientId = clientId,
                Content = "Good offer",
                Rating = 5
            };
            var res = await SendAsync(createCmd);
            return (offerId, hotelId, clientId, res);
        }



        [Test]
        public void GetShouldThrowExceptionIfOfferOrHotelIsNotFound()
        {
            FluentActions.Invoking(() =>
                SendAsync(new GetReviewsWithPaginationQuery()
                {
                    HotelID = -1,
                    OfferID = -123,
                })).Should().Throw<NotFoundException>();
        }
        [Test]
        public void GetShouldThrowExceptionIfInvalidPagination()
        {
            FluentActions.Invoking(() =>
                SendAsync(new GetReviewsWithPaginationQuery()
                {
                    PageNumber = -123,
                    PageSize = -1
                })).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task UpdateValidDataShouldUpdate()
        {
            var res = await CreateReview();
            var cmd = new UpdateReviewCmd()
            {
                Content = "2e1221123dasda",
                Rating = 2,
                CreationDate = DateTime.Now,
                ReviewID = res.reviewID,
                OfferId = res.offerId,
                HotelId = res.hotelId,
                ClientId = res.clientId
            };
            await SendAsync(cmd);
            var review = context.Reviews.Find(res.reviewID);

            Assert.AreEqual(review.Content, cmd.Content);
            Assert.AreEqual(review.Rating, cmd.Rating);
        }
        [Test]
        public async Task UpdateWrongClientShouldThrow()
        {
            var res = await CreateReview();
            var cmd = new UpdateReviewCmd()
            {
                Content = "2e1221123dasda",
                Rating = 2,
                CreationDate = DateTime.Now,
                ReviewID = res.reviewID,
                OfferId = res.offerId,
                HotelId = res.hotelId,
                ClientId = 123123
            };
            FluentActions.Invoking(() => SendAsync(cmd))
            .Should().Throw<ForbiddenAccessException>();
        }
        [Test]
        public async Task UpdateWrongReviewIdShouldThrow()
        {
            var res = await CreateReview();
            var cmd = new UpdateReviewCmd()
            {
                Content = "2e1221123dasda",
                Rating = 2,
                CreationDate = DateTime.Now,
                ReviewID = 123123123,
                OfferId = res.offerId,
                HotelId = res.hotelId,
                ClientId = res.clientId
            };
            FluentActions.Invoking(() => SendAsync(cmd))
            .Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task CreateValidDataShouldCreateReview()
        {
            var res = await CreateReview();
            res.reviewID.Should().BeGreaterOrEqualTo(1);
        }
        [Test]
        public async Task CreateInvalidDataShouldThrowValidationException()
        {
            (int offerId, int hotelId, int clientId) = await CreateHotelAndOffer();

            var createCmd = new CreateReviewCmd()
            {
                HotelID = hotelId,
                OfferID = offerId,
                ClientId = clientId,
                Content = "",
                Rating = 12
            };
            FluentActions.Invoking(() => SendAsync(createCmd))
            .Should().Throw<ValidationException>();
        }
        [Test]
        public async Task CreateWrongResourceShouldThrowNotFound()
        {
            (int offerId, int hotelId, int clientId) = await CreateHotelAndOffer();

            var createCmd = new CreateReviewCmd()
            {
                HotelID = hotelId,
                OfferID = 12312312,
                ClientId = clientId,
                Content = "",
                Rating = 12
            };
            FluentActions.Invoking(() => SendAsync(createCmd))
            .Should().Throw<ValidationException>();
        }
        [Test]
        public async Task DeleteValidDataShouldDeleteReview()
        {
            var data = await CreateReview();
            var deleteCmd = new DeleteReviewCmd()
            {
                ReviewId = data.reviewID,
                HotelId = data.hotelId,
                OfferId = data.offerId,
                ClientId = data.clientId
            };
            await SendAsync(deleteCmd);
        }
        [Test]
        public async Task DeleteWrongIdShouldThrow()
        {
            var data = await CreateReview();
            var deleteCmd = new DeleteReviewCmd()
            {
                ReviewId = 123123,
                HotelId = data.hotelId,
                OfferId = data.offerId,
                ClientId = data.clientId
            };
            FluentActions.Invoking(() => SendAsync(deleteCmd))
            .Should().Throw<NotFoundException>();
        }
        [Test]
        public async Task DeleteWrongClientShouldThrow()
        {
            var data = await CreateReview();
            var deleteCmd = new DeleteReviewCmd()
            {
                ReviewId = data.reviewID,
                HotelId = data.hotelId,
                OfferId = data.offerId,
                ClientId = 3213123
            };
            FluentActions.Invoking(() => SendAsync(deleteCmd))
            .Should().Throw<ForbiddenAccessException>();
        }

        [Test]
        public async Task GetShouldReturnReviews()
        {
            (int offerId, int hotelId, int clientId) = await CreateHotelAndOffer();

            var reviewId = await SendAsync(new CreateReviewCmd()
            {
                HotelID = hotelId,
                OfferID = offerId,
                ClientId = clientId,
                Content = "Cool offer",
                Rating = 5
            });
            var result = await SendAsync(new GetReviewsWithPaginationQuery()
            {
                HotelID = hotelId,
                OfferID = offerId
            });

            result.Should().NotBeNull();
            result.TotalPages.Should().Be(1);
            result.TotalCount.Should().Be(1);
            result.Items.Should().NotBeNull();
            result.Items.Count.Should().Be(1);
            result.Items.Any(x => x.Id == reviewId).Should().BeTrue();
        }
    }
}