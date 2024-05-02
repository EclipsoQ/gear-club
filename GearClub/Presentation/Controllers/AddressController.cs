using GearClub.Areas.Identity.Data;
using GearClub.Domain.Models;
using GearClub.Domain.ServiceInterfaces;
using GearClub.Infrastructures.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GearClub.Presentation.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;        
        public AddressController(IAddressService addressService, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.addressService = addressService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index(string id)
        {
            var addresses = addressService.GetAddressesByUser(id);
            if (!User.IsInRole("Admin"))
            {
                return View("ClientIndex", addresses);
            }
            return View("AdminIndex", addresses);
        }

        public IActionResult Detail(int id)
        {
            var address = addressService.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("Admin"))
            {
                return View("ClientDetail", address);
            }
            return View("AdminDetail", address);            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var address = addressService.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("Admin"))
            {
                return View("ClientEdit", address);
            }
            return View("AdminEdit", address);
        }

        [HttpPost]
        public IActionResult Edit(Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            if (addressService.UpdateAddress(address))
            {
                if(!User.IsInRole("Admin"))
                {
                    return View("ClientDetail", address);
                }
                return View("AdminEdit", address);
            }
            return StatusCode(500, "Error processing action");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var address = addressService.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("Admin"))
            {
                return View("ClientDelete", address);
            }
            return View("AdminDelete", address);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int AddressId)
        {
            var address = addressService.GetAddressById(AddressId);
            if (address == null)
            {
                return BadRequest();
            }
            if (addressService.DeleteAddress(address))
            {
                if (!User.IsInRole("Admin"))
                {
                    return Redirect("/Address/Index/" + userManager.GetUserId(User));
                }
                return Redirect("/Address/Index/" + userManager.GetUserId(User));
            }
            return StatusCode(500, "Error processing action");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return View("ClientCreate");
            }
            return View("AdminCreate");
        }
        [HttpPost] 
        public IActionResult Create(Address address)
        {
            ViewBag.UserId = userManager.GetUserId(User);
            if (addressService.CreateAddress(address))
            {
                if (!User.IsInRole("Admin"))
                {
                    return Redirect("/Address/Index/" + userManager.GetUserId(User));
                }
                return Redirect("/Address/Index/" + userManager.GetUserId(User));
            }
            return View("ClientCreate", address);                        
        }        
    }
}
