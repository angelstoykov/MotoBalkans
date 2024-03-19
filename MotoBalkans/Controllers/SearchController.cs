using Microsoft.AspNetCore.Mvc;
using MotoBalkans.Data;
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
            var availableMotorcycles = new AvailableMotorcyclesViewModel(1, "peho", "mesho", 3);
            var list = new List<AvailableMotorcyclesViewModel>();
            list.Add(availableMotorcycles);
            return View("AvailableMotorcycles", list);
        }
    }
}
