using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using GearClub.Infrastructures.ExtensionMethods;
using GearClub.Domain.Models;
using GearClub.Application.Services;
using GearClub.Presentation.CompositeModels;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;

namespace GearClub.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult ViewProducts(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;
            return View(productService.GetAllProducts().ToPagedList(pageNumber, pageSize));
        }

        public IActionResult ProductDetails(int id)
        {
            return View(productService.GetProductDetail(id));
        }
        
        public IActionResult ProductsByCategory(int? page, int id)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;
            ViewData["CategoryId"] = id;
            return View(productService.GetProductsByCategory(id)
                .ToPagedList(pageNumber, pageSize));
        }
    }
}
