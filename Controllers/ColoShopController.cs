using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers
{
    public class ColoShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
