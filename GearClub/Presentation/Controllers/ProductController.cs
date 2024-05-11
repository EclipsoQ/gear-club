using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using GearClub.Infrastructures.ExtensionMethods;
using GearClub.Domain.Models;
using GearClub.Application.Services;
using GearClub.Presentation.CompositeModels;
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

        [HttpGet]
        public IActionResult SearchProduct(int? page, string? search)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;
            return View(productService.SearchProduct(search).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult FilterProduct(int? page, [FromBody]FilterData filterData)
        {
            int pageNumber = page ?? 1;
            int pageSize = 100;
            return View(productService.FilterProduct(filterData)
                .ToPagedList(pageNumber, pageSize));
        }
    }
}
