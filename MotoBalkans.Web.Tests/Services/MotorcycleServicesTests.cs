using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MotoBalkans.Data;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Data.Enums;
using MotoBalkans.Data.Models;
using MotoBalkans.Services;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Controllers;
using MotoBalkans.Web.Models.ViewModels;
using Xunit;

namespace MotoBalkans.Tests.Services
{
    public class MotorcycleServicesTests
    {
        private Fixture _fixture;
        private readonly MotorcycleService _motorcycleService;
        private readonly Mock<IRepository<Motorcycle>> _mockMotorcycleRepository;
        private readonly Mock<IRepository<Engine>> _mockEngineRepository;
        private readonly Mock<IRepository<Transmission>> _mockTransmissionRepository;
        private readonly Mock<IRepository<Rental>> _mockRentalRepository;
        private readonly Mock<IAvailabilityChecker> _mockAvailabilityChecker;

        private MotoBalkansDbContext _dbContext;

        private readonly Mock<IMotorcycleService> _mockMotorcycleService;


        public MotorcycleServicesTests()
        {
            _fixture = new Fixture();

            _mockMotorcycleRepository = new Mock<IRepository<Motorcycle>>();
            _mockEngineRepository = new Mock<IRepository<Engine>>();
            _mockTransmissionRepository = new Mock<IRepository<Transmission>>();
            _mockRentalRepository = new Mock<IRepository<Rental>>();
            _mockAvailabilityChecker = new Mock<IAvailabilityChecker>();

            _mockMotorcycleService = new Mock<IMotorcycleService>();

            _motorcycleService = new MotorcycleService(
                _mockMotorcycleRepository.Object,
                _mockEngineRepository.Object,
                _mockTransmissionRepository.Object,
                _mockRentalRepository.Object,
                _mockAvailabilityChecker.Object);

            SetupDbContext();
        }

        private void SetupDbContext()
        {
            _fixture = new Fixture();
            var options = new DbContextOptionsBuilder<MotoBalkansDbContext>().Options;

            _dbContext = new MotoBalkansDbContext(options);

            _fixture.Inject(_dbContext);
        }

        [Fact]
        public async Task GetPaginatedMotorcycleResult_ReturnsPaginatedList()
        {
            // Arrange
            var motorcycles = new List<Motorcycle>
        {
            new Motorcycle { Id = 1, Brand = "Honda", Model = "CBR1000RR" },
            new Motorcycle { Id = 2, Brand = "Yamaha", Model = "R1" },
            new Motorcycle { Id = 3, Brand = "Ducati", Model = "Panigale V4" }
        };

            _mockMotorcycleRepository.Setup(repo => repo.GetAll())
                                     .ReturnsAsync(motorcycles);

            // Act
            var result = await _motorcycleService.GetPaginatedMotorcycleResult(1, "brand_name_desc", 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count()); // Assuming we have 3 motorcycles in the list
            Assert.Equal("Yamaha", result.First().Brand); // Asserting the first item based on descending brand name order
        }

        [Fact]
        public async Task GetPaginatedMotorcycleResult_DefaultSortOrder()
        {
            // Arrange
            var motorcycles = new List<Motorcycle>
        {
            new Motorcycle { Id = 1, Brand = "Honda", Model = "CBR1000RR" },
            new Motorcycle { Id = 2, Brand = "Yamaha", Model = "R1" },
            new Motorcycle { Id = 3, Brand = "Ducati", Model = "Panigale V4" }
        };

            _mockMotorcycleRepository.Setup(repo => repo.GetAll())
                                     .ReturnsAsync(motorcycles);

            // Act
            var result = await _motorcycleService.GetPaginatedMotorcycleResult(1, "", 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count()); // Assuming we have 3 motorcycles in the list
            Assert.Equal("Ducati", result.First().Brand); // Asserting the first item based on default sorting (by brand ascending)
        }

        [Fact]
        public async Task Add_ReturnsCorrectViewModel_WhenServiceReturnsData()
        {
            // Arrange
            var engineTypes = new List<Engine>
            {
                new Engine { Id = 1, EngineType = EngineType.Electric },
                new Engine { Id = 2, EngineType = EngineType.Conbustion }
            };

            var transmissionTypes = new List<Transmission>
            {
                new Transmission { Id = 1, TransmissionType = TransmissionType.Manual },
                new Transmission { Id = 2, TransmissionType = TransmissionType.Automatic }
            };

            _mockMotorcycleService
                .Setup(s => s.GetEngineTypes())
                .ReturnsAsync(engineTypes);

            _mockMotorcycleService
                .Setup(s => s.GetTransmissionTypes())
            .ReturnsAsync(transmissionTypes);

            var controller = new MotorcycleController(_dbContext, _mockMotorcycleService.Object);

            // Act
            var result = await controller.Add();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<AddNewMotocycleFormViewModel>(viewResult.Model);

            Assert.NotNull(viewModel.EngineTypes);
            Assert.NotNull(viewModel.TransmissionTypes);

            Assert.Equal(2, viewModel.EngineTypes.Count());
            Assert.Equal(2, viewModel.TransmissionTypes.Count());
        }

        [Fact]
        public async Task Add_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            var viewModel = new AddNewMotocycleFormViewModel();
            var controller = new MotorcycleController(_dbContext, _mockMotorcycleService.Object);

            controller.ModelState.AddModelError("Brand", "Brand is required");

            // Act
            var result = await controller.Add(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
        }

        [Fact]
        public async Task Add_CreatesNewMotorcycleAndRedirects_WhenModelStateIsValid()
        {
            // Arrange
            var viewModel = new AddNewMotocycleFormViewModel
            {
                Brand = "Honda",
                Model = "CBR500R",
                EngineId = 1,
                TransmissionId = 2,
                PictureUrl = "https://example.com/image.jpg",
                PricePerDay = 50.00m
            };

            var controller = new MotorcycleController(_dbContext, _mockMotorcycleService.Object);

            var engineTypes = new List<Engine>
            {
                new Engine { Id = 1, EngineType = EngineType.Electric }
            };

            var transmissionTypes = new List<Transmission>
            {
                new Transmission { Id = 2, TransmissionType = TransmissionType.Manual }
            };

            _mockMotorcycleService
                .Setup(s => s.GetEngineTypes())
                .ReturnsAsync(engineTypes);

            _mockMotorcycleService
                .Setup(s => s.GetTransmissionTypes())
                .ReturnsAsync(transmissionTypes);

            // Act
            var result = await controller.Add(viewModel);

            // Assert
            _mockMotorcycleService.Verify(s => s.CreateNewMotorcycle(It.IsAny<Motorcycle>()), Times.Once);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectToActionResult.ActionName);
            Assert.Equal("Motorcycle", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Add_RedirectsToError_WhenEngineOrTransmissionTypesAreNull()
        {
            // Arrange
            var viewModel = new AddNewMotocycleFormViewModel();

            List<Engine> engineTypes = null;

            List<Transmission> transmissionTypes = null;

            _mockMotorcycleService
                .Setup(s => s.GetEngineTypes())
                .ReturnsAsync(engineTypes);

            _mockMotorcycleService
                .Setup(s => s.GetTransmissionTypes())
                .ReturnsAsync(transmissionTypes);

            var controller = new MotorcycleController(_dbContext, _mockMotorcycleService.Object);
            controller.ModelState.AddModelError("Brand", "Brand is required");

            // Act
            var result = await controller.Add(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("NotFound", redirectToActionResult.ActionName);
            Assert.Equal("Error", redirectToActionResult.ControllerName);
        }
    }
}
