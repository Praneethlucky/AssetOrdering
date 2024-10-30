using AssestOrderingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssestOrderingApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        public OrderService OrderService;

        public AdminController(AdminService adminService, OrderService orderService)
        {
            OrderService = orderService;
            _adminService = adminService;
        }


        public IActionResult Index()
        {
            ViewData["IsAdmin"] = IsAdmin();
            return View();
        }
        public bool IsAdmin()
        {
            return _adminService.IsAdmin(User.Identity.Name);
        }
        
    }
}
