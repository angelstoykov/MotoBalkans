using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Models;
using MotoBalkans.Web.Models.ViewModels;
using MotoBalkans.Web.Utilities.Contracts;

namespace MotoBalkans.Web.Controllers
{
    public class SearchController : Controller
    {
        private IMotoBalkansDbContext _data;
        private IAvailabilityChecker _checker;
        public SearchController(IMotoBalkansDbContext context,
                                IAvailabilityChecker checker)
        {
            _data = context;
            _checker = checker;
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
            var allMotorcycles = GetAllMotorcycles();
            var availableMotorcycles = new List<AvailableMotorcyclesViewModel>();

            foreach(var motorcycle in allMotorcycles)
            {
                var isMotocycleAvailable = _checker.IsMotorcycleAvailable(motorcycle.Id, startDate, endDate);
                if (isMotocycleAvailable)
                {
                    availableMotorcycles.Add(new AvailableMotorcyclesViewModel(motorcycle.Id, motorcycle.Model, motorcycle.Brand, 0));
                }
            }



           
            return availableMotorcycles;
        }

        private List<Motorcycle> GetAllMotorcycles()
        {
            return _data.Motorcycles
                .AsNoTracking()
                .Select(x => new Motorcycle()
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                })
                .ToList();
        }
    }
}
