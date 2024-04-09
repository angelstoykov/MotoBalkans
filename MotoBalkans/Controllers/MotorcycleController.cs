using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Enums;
using MotoBalkans.Web.Data.Models;
using MotoBalkans.Web.Models.ViewModels;
using System.Linq;

namespace MotoBalkans.Web.Controllers
{
    [Authorize]
    public class MotorcycleController : Controller
    {
        private MotoBalkansDbContext _data;
        private IMotorcycleService _motorcycleService;

        public MotorcycleController(MotoBalkansDbContext context, IMotorcycleService motorcycleService)
        {
            _data = context;
            _motorcycleService = motorcycleService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allMotorcyclesFromDb = await _motorcycleService.GetAllMotorcycles();

            var model = allMotorcyclesFromDb
                .Select(m => new AllMotorcyclesViewModel(
                    m.Id,
                    m.Brand,
                    m.Model,
                    m.PricePerDay
                    ))
                .ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddNewMotocycleFormViewModel();

            var engineTypes = await _motorcycleService.GetEngineTypes();

            viewModel.EngineTypes = engineTypes
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                });

            var transmissionTypes = await _motorcycleService.GetTransmissionTypes();
            viewModel.TransmissionTypes = transmissionTypes
                .Select(c => new TransmissionViewModel()
                {
                    Id = c.Id,
                    Type = c.TransmissionType
                });


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddNewMotocycleFormViewModel createMotorcycleModel)
        {
            if (!ModelState.IsValid)
            {
                var engineTypes = await _motorcycleService.GetEngineTypes();

                createMotorcycleModel.EngineTypes = engineTypes
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                });

                var transmissionTypes = await _motorcycleService.GetTransmissionTypes();
                createMotorcycleModel.TransmissionTypes = transmissionTypes
                    .Select(c => new TransmissionViewModel()
                    {
                        Id = c.Id,
                        Type = c.TransmissionType
                    });

                return View(createMotorcycleModel);
            }

            var motorcycle = new Motorcycle()
            {
                Brand = createMotorcycleModel.Brand,
                Model = createMotorcycleModel.Model,
                EngineId = createMotorcycleModel.EngineId,
                TransmissionId = createMotorcycleModel.TransmissionId
            };

            await _motorcycleService.CreateNewMotorcycle(motorcycle);
            
            return RedirectToAction("All", "Motorcycle");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleDetailsById(id);
            var detailsModel = new MotorcycleDetailsViewModel()
            {
                Id = motorcycle.Id,
                Brand = motorcycle.Brand,
                Model = motorcycle.Model,
                EngineType = motorcycle.Engine.EngineType,
                TransmissionType = motorcycle.Transmission.TransmissionType
            };

            return View(detailsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleById(id);
            if (motorcycle == null)
            {
                return BadRequest();
            }

            //if (motorcycle != GetUserId())
            //{
            //    return Unauthorized();
            //}

            var model = new DeleteMotorcycleViewModel()
            {
                Id = motorcycle.Id,
                Brand = motorcycle.Brand,
                Model = motorcycle.Model
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorcycle = await _motorcycleService.GetMotorcycleById(id);

            var rentals = await _motorcycleService.GetAllRentals();
            var filteredRentals = rentals.Where(r => r.MotorcycleId == id);


            if (motorcycle == null)
            {
                return BadRequest();
            }

            // if motocycle have rentals, delete them first
            if (filteredRentals.Count() > 0)
            {
                await _motorcycleService.DeleteRentals(filteredRentals);
            }

            await _motorcycleService.DeleteMotorcycle(motorcycle);

            return RedirectToAction("All", "Motorcycle");
        }
    }
}
