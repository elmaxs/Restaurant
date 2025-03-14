using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
