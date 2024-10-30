using Microsoft.AspNetCore.Mvc;
using AssestOrderingApplication.Models;
using AssestOrderingApplication.Services;

namespace AssestOrderingApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Index()
        {

            ViewData["IsAdmin"] = true;
            var cartItems = _cartService.GetCartItems(User.Identity.Name.Split('\\').Last());

            return View(cartItems);
        }
        
        public IActionResult AddtoCart(List<Cart> cart)
        {
            _cartService.InsertIntoCart(cart);
            _cartService.GetCartItems(ViewData["IsAdmin"].ToString());
            return View();
        }
    }
}
