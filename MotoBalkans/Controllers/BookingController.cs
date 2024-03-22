using Microsoft.AspNetCore.Mvc;
using MotoBalkans.Web.Data.Models;
using System.Security.Claims;

namespace MotoBalkans.Web.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Book(int id, string startDateRequested, string endDateRequested)
        {
            var userId = GetUserId();

            //var motorcycleId = id;

            //var rental = new Rental()
            //{
            //    CustomerId = userId,
            //    MotorcycleId = motorcycleId,
            //    StartDate = DateTime.Now,
            //};
            return View();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
