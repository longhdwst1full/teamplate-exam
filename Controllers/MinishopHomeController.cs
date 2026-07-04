using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class MinishopHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}
