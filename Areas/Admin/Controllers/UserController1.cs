using Microsoft.AspNetCore.Mvc;

namespace SkyLine.Areas.Admin.Controllers
{
    public class UserController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
