using AssestOrderingApplication.Models;
using AssestOrderingApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssestOrderingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AssetService _assetService;

        public HomeController(ILogger<HomeController> logger, AssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        public IActionResult Index()
        {
            
            return View(_assetService.GetAllAssets());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
