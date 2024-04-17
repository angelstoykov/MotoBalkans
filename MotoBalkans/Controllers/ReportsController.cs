using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Enums;
using MotoBalkans.Web.Extentions;
using MotoBalkans.Web.Models.ViewModels;

namespace MotoBalkans.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private Func<IApplicationUser> userResolverFactory;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public ReportsController(IReportService reportService, IApplicationUser applicationUser) : this (reportService)
        {
            userResolverFactory = () =>
            {
                return applicationUser;
            };
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var user = userResolverFactory();

            if (!user.IsInAdminRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var reportsList = await _reportService.GetAll();
            if (reportsList == null || reportsList.Count() == 0)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var viewModel = new AllReportsViewModel();

            foreach(var report in reportsList)
            {
                var reportViewModel = new ReportViewModel() { Id = report.Id, Name = report.Name };
                viewModel.Items.Add(reportViewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            if (!User.IsInAdminRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            if (id <= 0 || id.GetType() != typeof(int))
            {
                return RedirectToAction("BadRequest", "Error");
            }

            var result = await _reportService.CreateReport(id);

            if (result is null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var viewName = string.Empty;

            switch (result.ReportId)
            {
                case 1:
                    viewName = "AllRentalsReport";
                    break;
            }

            return View(viewName, result);
        }
    }
}
