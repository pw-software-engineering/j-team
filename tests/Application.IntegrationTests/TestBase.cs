using HotelReservationSystem.Application.Common.Interfaces;
using HotelReservationSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {

        private readonly IServiceScope scope;
        protected readonly IApplicationDbContext context;

        public TestBase()
        {
            this.scope = _scopeFactory.CreateScope();
            context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        }
        ~TestBase()
        {
            scope.Dispose();
        }
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
