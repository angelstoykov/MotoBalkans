using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MotoBalkans.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        [HttpGet]
        public IActionResult All()
        {
            return View();
        }
    }
}
