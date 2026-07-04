using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class MinishopShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ProductSingle()
    {
        return View();
    }

    public IActionResult Cart()
    {
        return View();
    }

    public IActionResult Checkout()
    {
        return View();
    }
}
