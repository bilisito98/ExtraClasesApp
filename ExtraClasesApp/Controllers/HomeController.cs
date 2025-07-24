using Microsoft.AspNetCore.Mvc;

namespace ExtraClasesApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Views/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return View(); // Views/Home/Privacy.cshtml
        }
    }
}
