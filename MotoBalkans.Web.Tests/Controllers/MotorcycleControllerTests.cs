using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using MotoBalkans.Data;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Controllers;
using MotoBalkans.Web.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MotoBalkans.Web.Tests.Controllers
{
    public class MotorcycleControllerTests
    {
        private Fixture _fixture;
        public MotorcycleControllerTests()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void Constructor_WithValidDependencies_ShouldInitialize()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MotoBalkansDbContext>()
                                .Options;

            var dbContext = new MotoBalkansDbContext(options);

            _fixture.Inject(dbContext);

            var mockMotorcycleService = new Mock<IMotorcycleService>();

            // Act
            var controller = new MotorcycleController(dbContext, mockMotorcycleService.Object);

            // Assert
            Assert.NotNull(controller);
            Assert.Same(dbContext, controller.GetContext());
            Assert.Same(mockMotorcycleService.Object, controller.GetMotorcycleService());
        }
    }
}
