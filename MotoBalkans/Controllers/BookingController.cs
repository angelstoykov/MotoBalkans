﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Data.Models;
using MotoBalkans.Web.Models.ViewModels;
using System.Security.Claims;

namespace MotoBalkans.Web.Controllers
{
    public class BookingController : Controller
    {
        private MotoBalkansDbContext _data;
        public BookingController(MotoBalkansDbContext context)
        {
            _data = context;
        }

        [HttpPost]
        public async Task<IActionResult> Book(AvailableMotorcyclesViewModel availableMotorcycle)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var rental = new Rental();
            var userId = GetUserId();

            rental.CustomerId = userId;
            rental.MotorcycleId = availableMotorcycle.Id;
            rental.StartDate = availableMotorcycle.StartDateRequested;
            rental.EndDate = availableMotorcycle.EndDateRequested;

            await _data.Rentals.AddAsync(rental);
            await _data.SaveChangesAsync();

            return RedirectToAction("Index", "Search");
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            string userId = GetUserId();

            var model = await _data.Rentals
                .Where(sp => sp.CustomerId == userId)
                .AsNoTracking()
                .ToListAsync();

            return null;
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
