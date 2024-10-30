using AssestOrderingApplication.Models;
using AssestOrderingApplication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssestOrderingApplication.Controllers
{
    public class AssetController : Controller
    {
        private readonly AssetService _assetService;
        private readonly OrderService _orderService;

        public AssetController(AssetService assetService, OrderService orderService)
        {
            _orderService = orderService;
            _assetService = assetService;
        }
        
        public IActionResult Index()
        {
            //var username = HttpContext.User.Identity?.Name;

            //if (string.IsNullOrEmpty(username))
            //{
            //    return Unauthorized("User is not authenticated.");
            //}

            //string actualUsername = username.Split('\\').Last();
            //var man = ActiveDirectoryHelper.GetManagerName(actualUsername);
            
            return View(GetAllAssests());
        }


        public IActionResult AddAsset([FromBody]Asset asset)
        {
            if (asset == null)
                return BadRequest("Oder is Empty");
            var result = _assetService.AddAsset(asset);
            if (result)
                return Ok(GetAllAssests());
            else
                return BadRequest("Couldnt add Asset");
        }

        public IActionResult RemoveAsset([FromBody]string assetId)
        {
            if (assetId == null)
                return BadRequest("Asset Id is Empty");
            var result = _assetService.DeleteAsset(assetId);
            if (result)
                return Ok(GetAllAssests());
            else
                return BadRequest("Couldnt add Asset");
        }
        public List<Asset> GetAllAssests()
        {
            return _assetService.GetAllAssets();
        }

        [HttpGet]
        public IActionResult Approval(string selectedStatus)
        {
            var order = _orderService.GetOrderByManager(User.Identity.Name.Split('\\').Last());
           
            if (!string.IsNullOrEmpty(selectedStatus))
            {
                order = order.Where(o => o.OrderStatus == selectedStatus).ToList();
            }

            // Populate the dropdown list with unique statuses
            ViewBag.Statuses = new SelectList(order.Select(o => o.OrderStatus).Distinct());
            return View(order);
        }
        [HttpPost]
        public IActionResult RequestApproval(string productFamily, string deliverTo, string address, string officeLocation, string country, string phoneNumber, string comments)
        {
            // Access the values directly
            // Example:
            var cartItems = _assetService.GetCartItems(User.Identity.Name.Split('\\').Last());
            Order order = new Order();
            order.ProductFamily = productFamily;
            order.PhoneNumber = phoneNumber;
            order.DeliverTo = deliverTo;
            order.HomeAddress = address;
            order.Country = country;
            order.OfficeAddress = officeLocation;
            order.Comments = comments;
            order.ManagerEmployeeId = User.Identity.Name.Split('\\').Last();
            order.AssetId = cartItems.Select(item => item.AssetId).ToList();
            order.AssetNames = string.Join(",", cartItems.Select(item => item.AssetName).ToList());
            order.EmployeeId = User.Identity.Name.Split('\\').Last();

            if(_assetService.insertIntoOrder(order))
                _assetService.DeleteFromCart(User.Identity.Name.Split('\\').Last());

            return RedirectToAction("OrderPlaced", "Asset");
        }
        [HttpPost]
        public IActionResult Request(Dictionary<string, List<string>> formData)
        {

            List<Cart> cartItems = new List<Cart>();
            // Process form data
            if (formData.Count == 1)
                return Index();
            foreach (var item in formData)
            {
                var parts = item.Value[0].ToString().Split('|');

                if (!item.Key.Contains("RequestVerificationToken"))
                {
                    Cart cart = new Cart();
                    cart.AssetName = parts[1];
                    cart.AssetQuantity = 1;
                    cart.AssetId = Int32.Parse(parts[0]);
                    cart.EmployeeName = User.Identity.Name.Split('\\').Last();
                    cartItems.Add(cart); // List of selected asset types
                }
            }
            _assetService.InsertIntoCart(cartItems);

            // Return a view or redirect
            return View(_assetService.GetCartItems(User.Identity.Name.Split('\\').Last()));
        }
        [HttpPost]
        public IActionResult DeleteandAdd(Dictionary<string, List<string>> formData)
        {

            List<Cart> cartItems = new List<Cart>();
            // Process form data
            if (formData.Count == 1)
                return Index();
            foreach (var item in formData)
            {
                var parts = item.Value[0].ToString().Split('|');

                if (!item.Key.Contains("RequestVerificationToken"))
                {
                    Cart cart = new Cart();
                    cart.AssetName = parts[1];
                    cart.AssetQuantity = 1;
                    cart.AssetId = Int32.Parse(parts[0]);
                    cart.EmployeeName = User.Identity.Name.Split('\\').Last();
                    cartItems.Add(cart); // List of selected asset types
                }
            }

            _assetService.DeleteFromCart(User.Identity.Name.Split('\\').Last());
            _assetService.InsertIntoCart(cartItems);

            // Return a view or redirect
            return RedirectToAction("Request","Asset");
        }
        public IActionResult CartEmpty()
        {
            return View();
        }
        public IActionResult OrderPlaced()
        {
            return View();
        }
        public IActionResult Request()
        {
            var cartItems = _assetService.GetCartItems(User.Identity.Name.Split('\\').Last());
            if(cartItems.Count > 0)
            {
                return View(_assetService.GetCartItems(User.Identity.Name.Split('\\').Last()));
            }
            return RedirectToAction("CartEmpty","Asset");
        }



        public IActionResult EditOrder()
        {
            var cartItems = _assetService.GetCartItems(User.Identity.Name.Split('\\').Last());
            var allAssests = GetAllAssests();

            foreach (var itemA in allAssests)
            {
                if (cartItems.Any(b => b.AssetId == itemA.Id))
                {
                    itemA.inCart = true;
                }
                else
                {
                    itemA.inCart = false;
                }
            }
            return View(allAssests);
        }

        public IActionResult ChangeStatus(int Id)
        {
            _assetService.InsertIntoCart(Id, "Approved");
            return RedirectToAction("Approval","Asset");
        }

    }


}
