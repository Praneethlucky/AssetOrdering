using AssestOrderingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssestOrderingApplication.Controllers
{
    public class OrderController : Controller
    {
        public OrderService OrderService;
        public OrderController(OrderService orderService)
        {
            OrderService = orderService;
        }
        public IActionResult Index()
        {
            var orders = OrderService.GetOrderById(User.Identity.Name.Split('\\').Last());
            return View(orders);
        }
    }
}
