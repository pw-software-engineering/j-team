﻿using HotelReservationSystem.Application.Common.Behaviours;
using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreateHotelCmd>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateHotelCmd>>();

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(x => x.UserId).Returns("Administrator");

            var requestLogger = new LoggingBehaviour<CreateHotelCmd>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreateHotelCmd { Name = "hotel1", City = "Warsaw", Country = "Poland", Description = "opis", HotelPreviewPicture = null, Pictures = new System.Collections.Generic.List<byte[]>()  }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateHotelCmd>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreateHotelCmd { Name = "hotel1", City = "Warsaw", Country = "Poland", Description = "opis", HotelPreviewPicture = null, Pictures = new System.Collections.Generic.List<byte[]>() }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}
