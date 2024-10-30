using Microsoft.AspNetCore.Mvc;

namespace AssestOrderingApplication.Controllers
{
    public class OrderStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
