using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Enums;
using MotoBalkans.Web.Data.Models;
using MotoBalkans.Web.Models.ViewModels;

namespace MotoBalkans.Web.Controllers
{
    public class MotorcycleController : Controller
    {
        private MotoBalkansDbContext _data;

        public MotorcycleController(MotoBalkansDbContext context, IMotorcycleService motorcycleService)
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
            var viewModel = new AddNewMotocycleFormViewModel();

            viewModel.EngineTypes = await GetEngineTypes();
            viewModel.TransmissionTypes = await GetTransmissionTypes();


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddNewMotocycleFormViewModel createMotorcycleModel)
        {
            if (!ModelState.IsValid)
            {
                createMotorcycleModel.EngineTypes = await GetEngineTypes();
                createMotorcycleModel.TransmissionTypes = await GetTransmissionTypes();

                return View(createMotorcycleModel);
            }

            var motorcycle = new Motorcycle()
            {
                Brand = createMotorcycleModel.Brand,
                Model = createMotorcycleModel.Model,
                EngineId = createMotorcycleModel.EngineId,
                TransmissionId = createMotorcycleModel.TransmissionId
            };

            await _data.Motorcycles.AddAsync(motorcycle);
            await _data.SaveChangesAsync();

            return RedirectToAction("All", "Motorcycle");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var detailsModel = await _data.Motorcycles
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(e => e.Engine)
                .Include(t => t.Transmission)
                .Select(m => new MotorcycleDetailsViewModel()
                {
                    Id = m.Id,
                    Brand = m.Brand,
                    Model = m.Model,
                    EngineType = m.Engine.EngineType,
                    TransmissionType = m.Transmission.TransmissionType,
                })
                .FirstOrDefaultAsync();

            return View(detailsModel);
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
