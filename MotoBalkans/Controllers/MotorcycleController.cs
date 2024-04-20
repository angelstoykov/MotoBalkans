using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoBalkans.Data;
using MotoBalkans.Data.Models;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Extentions;
using MotoBalkans.Web.Models.ViewModels;
using NuGet.Packaging;

namespace MotoBalkans.Web.Controllers
{
    [Authorize]
    public class MotorcycleController : Controller
    {
        private MotoBalkansDbContext _data;
        private IMotorcycleService _motorcycleService;

        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 5;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public MotoBalkansDbContext GetContext()
        {
            return _data;
        }

        public IMotorcycleService GetMotorcycleService()
        {
            return _motorcycleService;
        }

        public MotorcycleController(MotoBalkansDbContext context, IMotorcycleService motorcycleService)
        {
            _data = context;
            _motorcycleService = motorcycleService;
        }

        [HttpGet]
        public async Task<IActionResult> All(string sortOrder, int currentpage)
        {
            CurrentPage = currentpage == 0 ? CurrentPage : currentpage;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "brand_name_desc" : "";
            ViewData["ModelSortParam"] = sortOrder == "model_name_desc" ? "model_name_asc" : "model_name_desc";

            var paginatedMotorcyclesFromDb = await _motorcycleService.GetPaginatedMotorcycleResult(CurrentPage, sortOrder, PageSize);
            Count = await _motorcycleService.GetCount();

            var model = paginatedMotorcyclesFromDb
                .Select(m => new AllMotorcyclesViewModel(
                    m.Id,
                    m.Brand,
                    m.Model,
                    m.PricePerDay,
                    m.PictureUrl
                    ))
                .ToList();

            var viewModel = new AllMotorcyclesPaginatedViewModel()
            {
                CurrentPage = this.CurrentPage,
                TotalPages = this.TotalPages
            };

            viewModel.Items.AddRange(model);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddNewMotocycleFormViewModel();

            var engineTypes = await _motorcycleService.GetEngineTypes();
            var transmissionTypes = await _motorcycleService.GetTransmissionTypes();

            if (engineTypes == null || transmissionTypes == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            viewModel.EngineTypes = engineTypes
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                });

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
                var transmissionTypes = await _motorcycleService.GetTransmissionTypes();

                if (engineTypes == null || transmissionTypes == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                createMotorcycleModel.EngineTypes = engineTypes
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                });

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
                TransmissionId = createMotorcycleModel.TransmissionId,
                PictureUrl = createMotorcycleModel.PictureUrl,
                PricePerDay = createMotorcycleModel.PricePerDay
            };

            await _motorcycleService.CreateNewMotorcycle(motorcycle);

            return RedirectToAction("All", "Motorcycle");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id.GetType() != typeof(int)
                && id <= 0)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            var motorcycle = await _motorcycleService.GetMotorcycleDetailsById(id);

            if (motorcycle == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            var detailsModel = new MotorcycleDetailsViewModel()
            {
                Id = motorcycle.Id,
                Brand = motorcycle.Brand,
                Model = motorcycle.Model,
                EngineType = motorcycle.Engine.EngineType,
                TransmissionType = motorcycle.Transmission.TransmissionType,
                PricePerDay = motorcycle.PricePerDay,
                PictureUrl = motorcycle.PictureUrl
            };

            return View(detailsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInAdminRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var motorcycle = await _motorcycleService.GetMotorcycleById(id);
            if (motorcycle == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }

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
            if (!User.IsInAdminRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var motorcycle = await _motorcycleService.GetMotorcycleById(id);

            if (motorcycle == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            var rentals = await _motorcycleService.GetAllRentals();
            if (rentals.Any())
            {
                var filteredRentals = rentals.Where(r => r.MotorcycleId == id);

                // if motocycle have rentals, delete them first
                if (filteredRentals.Count() > 0)
                {
                    await _motorcycleService.DeleteRentals(filteredRentals);
                }
            }

            await _motorcycleService.DeleteMotorcycle(motorcycle);

            return RedirectToAction("All", "Motorcycle");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!User.IsInAdminRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var motorcycle = await _motorcycleService.GetMotorcycleById(id);

            if (motorcycle == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            var model = new EditMotorcycleViewModel()
            {
                Brand = motorcycle.Brand,
                Model = motorcycle.Model,
                PricePerDay = motorcycle.PricePerDay,
                EngineId = motorcycle.EngineId,
                TransmissionId = motorcycle.TransmissionId,
                PictureUrl = motorcycle.PictureUrl
            };

            var engineTypes = await _motorcycleService.GetEngineTypes();

            if (engineTypes == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            model.EngineTypes = engineTypes
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                });

            var transmissionTypes = await _motorcycleService.GetTransmissionTypes();

            if (transmissionTypes == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            model.TransmissionTypes = transmissionTypes
                .Select(c => new TransmissionViewModel()
                {
                    Id = c.Id,
                    Type = c.TransmissionType
                });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMotorcycleViewModel editModel, int id)
        {
            if (!User.IsInAdminRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var motorcycle = await _motorcycleService.GetMotorcycleById(id);

            if (motorcycle == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            if (!ModelState.IsValid)
            {
                var engineTypes = await _motorcycleService.GetEngineTypes();
                editModel.EngineTypes = engineTypes
                .Select(c => new EngineViewModel()
                {
                    Id = c.Id,
                    Type = c.EngineType
                });

                var transmissionTypes = await _motorcycleService.GetTransmissionTypes();
                editModel.TransmissionTypes = transmissionTypes
                    .Select(c => new TransmissionViewModel()
                    {
                        Id = c.Id,
                        Type = c.TransmissionType
                    });

                return View(editModel);
            }

            motorcycle.Brand = editModel.Brand;
            motorcycle.Model = editModel.Model;
            motorcycle.PricePerDay = editModel.PricePerDay;
            motorcycle.EngineId = editModel.EngineId;
            motorcycle.TransmissionId = editModel.TransmissionId;
            motorcycle.PictureUrl = editModel.PictureUrl;

            await _motorcycleService.EditMotorcycle(motorcycle);

            return RedirectToAction(nameof(All));
        }
    }
}
