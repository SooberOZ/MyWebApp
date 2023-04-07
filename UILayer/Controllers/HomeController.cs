using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UILayer.Models;

namespace UILayer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}