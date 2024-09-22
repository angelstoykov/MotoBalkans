using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MotoBalkans.Data;
using MotoBalkans.Data.Enums;
using MotoBalkans.Data.Models;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Controllers;
using MotoBalkans.Web.Models.ViewModels;
using Xunit;

namespace MotoBalkans.Tests.Web.Controllers
{
    public class MotorcycleControllerTests
    {
        private Fixture _fixture;
        private MotoBalkansDbContext _dbContext;
        public MotorcycleControllerTests()
        {
            _fixture = new Fixture();
            var options = new DbContextOptionsBuilder<MotoBalkansDbContext>().Options;

            _dbContext = new MotoBalkansDbContext(options);

            _fixture.Inject(_dbContext);
        }
        [Fact]
        public void Constructor_WithValidDependencies_ShouldInitialize()
        {
            // Arrange
            var mockMotorcycleService = new Mock<IMotorcycleService>();

            // Act
            var controller = new MotorcycleController(_dbContext, mockMotorcycleService.Object);

            // Assert
            Assert.NotNull(controller);
            Assert.Same(_dbContext, controller.GetContext());
            Assert.Same(mockMotorcycleService.Object, controller.GetMotorcycleService());
        }

        [Fact]
        public async Task Add_ReturnsViewResult_WithViewModel()
        {
            // Arrange
            var mockMotorcycleService = new Mock<IMotorcycleService>();
            var controller = new MotorcycleController(_dbContext, mockMotorcycleService.Object);

            var engineTypes = new List<Engine>
            {
                new Engine()
                {
                    Id = 1,
                    EngineType = EngineType.Electric,
                    Size = 750
                },
                new Engine()
                {
                    Id = 2,
                    EngineType = EngineType.Conbustion,
                    Size = 950
                }
            };

            var transmissionTypes = new List<Transmission>
            {
                new Transmission()
                {
                    Id = 1,
                    TransmissionType = TransmissionType.Manual
                },
                new Transmission()
                {
                    Id = 2,
                    TransmissionType = TransmissionType.Automatic
                }
            };

            mockMotorcycleService.Setup(s => s.GetEngineTypes()).ReturnsAsync(engineTypes);
            mockMotorcycleService.Setup(s => s.GetTransmissionTypes()).ReturnsAsync(transmissionTypes);

            // Act
            var result = await controller.Add();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<AddNewMotocycleFormViewModel>(viewResult.Model);

            Assert.NotNull(viewModel);
            Assert.Equal(2, viewModel.EngineTypes.Count());
            Assert.Equal(2, viewModel.TransmissionTypes.Count());
        }

        [Fact]
        public async Task Add_ReturnsRedirectToActionResult_WhenServiceReturnsNull()
        {
            // Arrange
            var mockMotorcycleService = new Mock<IMotorcycleService>();
            var controller = new MotorcycleController(_dbContext, mockMotorcycleService.Object);

            List<Engine> engineTypes = null;

            List<Transmission> transmissionTypes = null;

            mockMotorcycleService.Setup(s => s.GetEngineTypes()).ReturnsAsync(engineTypes);
            mockMotorcycleService.Setup(s => s.GetTransmissionTypes()).ReturnsAsync(transmissionTypes);

            // Act
            var result = await controller.Add();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("NotFound", redirectToActionResult.ActionName);
            Assert.Equal("Error", redirectToActionResult.ControllerName);
        }
    }
}
