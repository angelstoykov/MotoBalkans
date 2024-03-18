using Microsoft.AspNetCore.Mvc;

namespace MotoBalkans.Web.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
