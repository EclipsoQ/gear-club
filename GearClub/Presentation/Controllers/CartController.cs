using GearClub.Application.Services;
using GearClub.Areas.Identity.Data;
using GearClub.Domain.Models;
using GearClub.Domain.ServiceInterfaces;
using GearClub.Presentation.CompositeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace GearClub.Presentation.Controllers
{    
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICartService cartService;
        public CartController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ICartService cartService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.cartService = cartService;
        }        
        public async Task<IActionResult> Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);
                var cart = cartService.GetCartByUser(user.Id);
                if (cart == null)
                {
                    if (cartService.CreateCart(user.Id))
                    {
                        cart = cartService.GetCartByUser(user.Id);
                    }
                    else return StatusCode(500, "Error creating cart");
                }
                var cartDetails = cartService.GetAllDetail(cart.CartId);
                if (!cartDetails.CartDetails.Any())
                {
                    return View("PartialViews/_EmptyCartPartial");
                }
                return View(cartDetails);
            }
            else return View("PartialViews/_EmptyCartPartial");
        }

        [HttpPost]                        
        public IActionResult UpdateCartLine([FromBody] CartLineModel data)
        {
            var id = data.Id;
            var quantity = data.Quantity;
            if (cartService.UpdateLine(id, quantity))
            {                                                
                return Ok();
            }
            else return StatusCode(500, "Error processing data");            
        }

        [HttpPost]
        public IActionResult RemoveCartLine([FromBody] CartLineModel data)
        {
            var id = data.Id;
            if (cartService.RemoveLine(id))
            {
                return Ok();
            }
            else return StatusCode(500, "Internal Server Error");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart([FromBody] CartDetail line)
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = userManager.GetUserId(User);                
                if (cartService.GetCartByUser(user) == null)
                {
                    cartService.CreateCart(user);                    
                }

                var cart = cartService.GetCartByUser(user);
                
                var lineToUpdate = new CartDetail()
                {
                    CartId = cart.CartId,
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                };
                if (cartService.AddToCart(lineToUpdate))
                {
                    return Ok();
                }
                else return StatusCode(500, "Internal Server Error");
            }
            return View("UnauthorizedView");
        }
    }
}
