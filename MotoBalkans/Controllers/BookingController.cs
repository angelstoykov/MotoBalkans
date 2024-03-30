using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Data.Constants;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Models;
using System.Globalization;
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

        [HttpGet]
        public async Task<IActionResult> Book(int id, string startDateRequested, string endDateRequested)
        {
            var rental = new Rental();
            var userId = GetUserId();
            var culture = new CultureInfo("bg-BG");

            var motorcycleId = id;

            var startDateRequestedParsed = DateTime.Now;
            var endDateRequestedParsed = DateTime.Now;
            if (!DateTime.TryParse(startDateRequested,
                 culture,
                 DateTimeStyles.None,
                 out startDateRequestedParsed))
            {
                ModelState.AddModelError(nameof(rental.StartDate), ValidationMessages.DateIsInWrongFormat);
            }

            if (!DateTime.TryParse(endDateRequested,
                 culture,
                 DateTimeStyles.None,
                 out endDateRequestedParsed))
            {
                ModelState.AddModelError(nameof(rental.EndDate), ValidationMessages.DateIsInWrongFormat);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            rental.CustomerId = userId;
            rental.MotorcycleId = motorcycleId;
            rental.StartDate = startDateRequestedParsed;
            rental.EndDate = endDateRequestedParsed;

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
