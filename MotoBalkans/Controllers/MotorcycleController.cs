using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Models.ViewModels;
using System.Xml.Linq;

namespace MotoBalkans.Web.Controllers
{
    public class MotorcycleController : Controller
    {
        private MotoBalkansDbContext _data;

        public MotorcycleController(MotoBalkansDbContext context)
        {
            _data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var motorcycles = await _data.Motorcycles
                .AsNoTracking()
                .Select(m => new AllMotorcyclesViewModel(
                    m.Id,
                    m.Brand,
                    m.Model,
                    99
                    ))
                .ToListAsync();

            return View(motorcycles);
        }
    }
}
