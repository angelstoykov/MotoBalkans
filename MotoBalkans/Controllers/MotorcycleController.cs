using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Data.Enums;
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var engineTypes = await GetEngineTypes();
            var transmissionTypes = await GetTransmissionTypes();

            return View();
        }

        private async Task<IEnumerable<TransmissionViewModel>> GetTransmissionTypes()
        {
            return await _data
                .Transmissions
                .AsNoTracking()
                .Select(c => new TransmissionViewModel()
                {
                    Id = c.Id,
                    Type = c.TransmissionType
                })
                .ToListAsync();
        }

        private async Task<IEnumerable<EngineViewModel>> GetEngineTypes()
        {
            return await _data
                .Engines
                .AsNoTracking()
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                })
                .ToListAsync();
        }
    }
}
