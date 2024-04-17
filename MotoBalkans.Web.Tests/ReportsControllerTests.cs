using Microsoft.AspNetCore.Mvc;
using Moq;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Data.Models;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Controllers;
using MotoBalkans.Web.Models.ViewModels;
using Xunit;

namespace MotoBalkans.Web.Tests
{
    public class ReportsControllerTests
    {
        [Fact]
        public async void All_ReturnsCorrectView_WhenUserIsAdminAndReportsExist()
        {
            // Arrange
            var mockReportService = new Mock<IReportService>();
            var reportsList = new List<Report>
            {
            new Report { Id = 1, Name = "Report 1" },
            new Report { Id = 2, Name = "Report 2" }
        };
            mockReportService.Setup(service => service.GetAll()).ReturnsAsync(reportsList);

            var mockUser = new Mock<IApplicationUser>();

            mockUser.Setup(user => user.IsInAdminRole()).Returns(true);

            var controller = new ReportsController(mockReportService.Object);

            // Act
            var result = await controller.All();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<AllReportsViewModel>(viewResult.Model);
            Assert.Equal(2, viewModel.Items.Count); // Check reports count
        }

        [Fact]
        public async void All_ReturnsNotAuthorized_WhenUserIsNotAdmin()
        {
            // Arrange
            var mockReportService = new Mock<IReportService>();
            var mockUser = new Mock<IApplicationUser>();
            mockUser.Setup(user => user.IsInAdminRole()).Returns(false);

            var controller = new ReportsController(mockReportService.Object);

            // Act
            var result = await controller.All();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("NotAuthorized", redirectToActionResult.ActionName);
            Assert.Equal("Error", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task All_ReturnsNotFound_WhenNoReportsExist()
        {
            // Arrange
            var mockReportService = new Mock<IReportService>();
            mockReportService.Setup(service => service.GetAll()).ReturnsAsync(new List<Report>());

            var mockUser = new Mock<IApplicationUser>();
            mockUser.Setup(user => user.IsInAdminRole()).Returns(true);

            var controller = new ReportsController(mockReportService.Object);

            // Act
            var result = await controller.All();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("NotFound", redirectToActionResult.ActionName);
            Assert.Equal("Error", redirectToActionResult.ControllerName);
        }
    }
}
