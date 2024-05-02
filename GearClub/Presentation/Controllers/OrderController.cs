using GearClub.Areas.Identity.Data;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GearClub.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        public OrderController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOrderService orderService, ICartService cartService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.orderService = orderService;
            this.cartService = cartService;
        }
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
                return View(cartService.GetAllDetail(cart.CartId));
            }
            return View("UnauthorizedView");
        }
    }
}
