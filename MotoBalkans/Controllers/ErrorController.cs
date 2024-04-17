using Microsoft.AspNetCore.Mvc;

namespace MotoBalkans.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error/Home")]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet("/Error/NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }

        [HttpGet("/Error/BadRequest")]
        public IActionResult BadRequest()
        {
            return View();
        }

        [HttpGet("/Error/NotAuthorized")]
        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
