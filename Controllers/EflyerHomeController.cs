using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class EflyerHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Fashion()
    {
        return View();
    }

    public IActionResult Electronic()
    {
        return View();
    }

    public IActionResult Jewellery()
    {
        return View();
    }
}
