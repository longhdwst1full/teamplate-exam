using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class FootwearShopController : Controller
{
    public IActionResult Men()
    {
        return View();
    }

    public IActionResult Women()
    {
        return View();
    }

    public IActionResult ProductDetail()
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

    public IActionResult OrderComplete()
    {
        return View();
    }

    public IActionResult Wishlist()
    {
        return View();
    }
}
