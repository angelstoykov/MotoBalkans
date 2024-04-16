﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Models.ViewModels;

namespace MotoBalkans.Web.Controllers
{
    public class SearchController : Controller
    {
        private IMotoBalkansDbContext _data;
        private IMotorcycleService _motorcycleService;

        public SearchController(IMotoBalkansDbContext context,
                                IMotorcycleService motorcycleService)
        {
            _data = context;
            _motorcycleService = motorcycleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var searchViewModel = new SearchViewModel();

            return View(searchViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var availableMotorcycles = _motorcycleService.GetAvailableMotorcyclesForPeriod(model.StartDate, model.EndDate);
            var availableMotorcyclesViewModel = new List<AvailableMotorcyclesViewModel>();

            foreach(var item in availableMotorcycles.Result)
            {
                var availableMotorcycleViewModel = new AvailableMotorcyclesViewModel()
                {
                    Id = item.Id,
                    Brand = item.Brand,
                    Model = item.Model,
                    StartDateRequested = item.StartDateRequested,
                    EndDateRequested = item.EndDateRequested,
                    PricePerDay = item.PricePerDay
                };

                availableMotorcyclesViewModel.Add(availableMotorcycleViewModel);
            }
            
            return View("AvailableMotorcycles", availableMotorcyclesViewModel);
        }

        public async Task<IActionResult> SearchByString(string searchString)
        {
            ViewBag.TitleSuffix = searchString;

            var motorcycles = from m in _data.Motorcycles
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                motorcycles = motorcycles.Where(s => s.Brand!.Contains(searchString));
            }

            var result = await motorcycles.ToListAsync();

            var viewModel = new SearchByStringAllViewModel();
            foreach (var item in result)
            {

                var motorcycle = new SearchByStringViewModel(item.Id, item.Brand, item.Model, item.PricePerDay);

                viewModel.Items.Add(motorcycle);
            }

            return View(viewModel);
        }
    }
}
