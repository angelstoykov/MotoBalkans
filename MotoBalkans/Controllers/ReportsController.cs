using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Models.ViewModels;

namespace MotoBalkans.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var reportsList = await _reportService.GetAll();
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
            if (id == 0)
            {
                throw new Exception("No such report");
            }

            var result = await _reportService.CreateReport(id);

            if(result is null)
            {
                return NotFound();
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
