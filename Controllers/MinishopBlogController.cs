using Microsoft.AspNetCore.Mvc;

namespace ServerWeb.Controllers;

public class MinishopBlogController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Single()
    {
        return View();
    }
}
