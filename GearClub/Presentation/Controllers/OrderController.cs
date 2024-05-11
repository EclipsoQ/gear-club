using GearClub.Areas.Identity.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace GearClub.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly IAddressService addressService;
        public OrderController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOrderService orderService, ICartService cartService, IAddressService addressService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.orderService = orderService;
            this.cartService = cartService;
            this.addressService = addressService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return View("UnauthorizedView");
            }
            var user = userManager.GetUserId(User);
            if (user == null)
            {
                return View("NotFound"); 
            }
            var orders = orderService.GetOrdersByUser(user);
            if (User.IsInRole("Admin"))
            {
                return View("AdminIndex", orders);
            }
            return View("ClientIndex", orders);
        }

        [HttpGet]
        public IActionResult Detail (int id)
        {
            if (!signInManager.IsSignedIn(User))
            {
                return View("UnauthorizedView");
            }

            var orderDetails = orderService.GetOrderDetail(id);
            if (!User.IsInRole("Admin"))
            {
                return View("ClientDetails", orderDetails);
            }
            return View("AdminDetails", orderDetails);
        }

        [HttpGet]
        public IActionResult Checkout()
        {            
            if (signInManager.IsSignedIn(User))
            {
                var user = userManager.GetUserId(User);
                if (user == null)
                {
                    return NotFound();
                }
                var cart = cartService.GetCartByUser(user);
                if (cart == null)
                {
                    return View("NotFound");
                }
                ViewData["Addresses"] = addressService.GetAddressesByUser(user);
                return View(cartService.GetAllDetail(cart.CartId));
            }
            return View("UnauthorizedView");
        }
        
        [HttpPost]
        public IActionResult ProcessOrder([FromBody] Order order)
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = userManager.GetUserId(User);
                if (user == null || order == null)
                {
                    return BadRequest();
                }

                var cart = cartService.GetCartByUser(user);
                if (cart == null)
                {
                    return View("NotFound");
                }

                orderService.ProcessOrder(order, cart);
                return Ok();
            }
            return View("UnauthorizedView");
        }

        [HttpPost]
        public IActionResult CancelOrder (int OrderId)
        {
            if (!signInManager.IsSignedIn(User))
            {
                return View("UnauthorizedView");
            }
            
            if (orderService.CancelOrder(OrderId))
            {
                return RedirectToAction("Index");
            }
            else return StatusCode(500, "Internal server error");
        }
    }
}
