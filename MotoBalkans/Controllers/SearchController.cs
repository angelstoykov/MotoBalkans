using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Data.Models;
using MotoBalkans.Web.Models.ViewModels;

namespace MotoBalkans.Web.Controllers
{
    public class SearchController : Controller
    {
        private MotoBalkansDbContext _data;
        public SearchController(MotoBalkansDbContext context)
        {
            _data = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var searchViewModel = new SearchViewModel();

            return View(searchViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(DateTime startDate, DateTime endDate)
        {
            var availableMotorcycles = GetAvailableMotorcyclesForPeriod(startDate, endDate);
            return View("AvailableMotorcycles", availableMotorcycles);
        }

        private List<AvailableMotorcyclesViewModel> GetAvailableMotorcyclesForPeriod(DateTime startDate, DateTime endDate)
        {
            // TODO: This method is not finished yet.
            var checker = new AvailabilityChecker();
            var availableMotorcycles = new List<AvailableMotorcyclesViewModel>();
           
            return availableMotorcycles;
        }
    }
}
