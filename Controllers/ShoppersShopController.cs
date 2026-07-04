using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class ShoppersShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ShopSingle()
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

    public IActionResult ThankYou()
    {
        return View();
    }
}
