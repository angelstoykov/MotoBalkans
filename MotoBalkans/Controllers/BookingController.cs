using Microsoft.AspNetCore.Mvc;

namespace MotoBalkans.Web.Controllers
{
    public class BookingController : Controller
    {
        public async Task<IActionResult> Book(int motorcycleId)
        {
            return View();
        }
    }
}
