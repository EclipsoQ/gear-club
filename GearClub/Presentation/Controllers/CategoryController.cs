using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using GearClub.Areas.Identity.Data;
using GearClub.Presentation.CompositeModels;

namespace GearClub.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public CategoryController(ICategoryService categoryService, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var categories = categoryService.GetAllCategories();
                return View(categories);
            }
            return View("UnauthorizedView");
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var category = categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return View("NotFound");
                }
                return View(category);
            }
            return View("UnauthorizedView");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var category = categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return View("NotFound");
                }
                CategoryViewModel model = new CategoryViewModel()
                {
                    Category = category,
                }; 
                return View(model);
            }
            return View("UnauthorizedView");
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            var category = model.Category;
            if (category == null)
            {
                return View("NotFound");
            }
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                categoryService.UpdateCategory(model);
                return RedirectToAction("Index");
            }
            return View("UnauthorizedView");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {                
                return View();
            }
            return View("UnauthorizedView");
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            var category = model.Category;
            if (category == null)
            {
                return View("NotFound");
            }
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                categoryService.CreateCategory(model);
                return RedirectToAction("Index");
            }
            return View("UnauthorizedView");
        }


        [HttpGet]   
        public IActionResult Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var category = categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return View("NotFound");
                }
                categoryService.DeleteCategory(category);
                return View(category);
            }
            return View("UnauthorizedView");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category == null)
            {
                return View("NotFound");
            }
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                categoryService.DeleteCategory(category);
                return RedirectToAction("Index");
            }
            return View("UnauthorizedView");
        }
    }
}
