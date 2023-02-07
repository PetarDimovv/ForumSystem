using Microsoft.AspNetCore.Mvc;

namespace StackOverflow.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Posts()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult Users()
        {
            return this.View();
        }
    }
}
