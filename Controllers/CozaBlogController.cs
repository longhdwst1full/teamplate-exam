using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers
{
    public class CozaBlogController : Controller
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
