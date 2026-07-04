using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class ColoHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}
