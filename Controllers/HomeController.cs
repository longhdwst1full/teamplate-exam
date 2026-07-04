using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
