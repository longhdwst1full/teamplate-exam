using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers
{
    public class CozaShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}
