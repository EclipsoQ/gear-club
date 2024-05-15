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
            return View(productService.GetAllProducts().Where(p => p.Deleted_at == null)
                .ToPagedList(pageNumber, pageSize));
        }

        public IActionResult ProductDetails(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var products = productService.GetProductById(id);
                return View("Details", products);
            }            
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

        [HttpGet]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var products = productService.GetAllProducts();
                return View(products);
            }
            return View("UnauthorizedView");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var product = productService.GetProductById(id);
                if (product == null)
                {
                    return View("NotFound");
                }
                ProductViewModel model = new ProductViewModel()
                {
                    Product = product,
                };
                return View(model);
            }
            return View("UnauthorizedView");            
        }

        [HttpPost]
        public IActionResult Edit (ProductViewModel model)
        {            
            var product = model.Product;
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {                
                if (product == null)
                {
                    return View("NotFound");
                }
                if (productService.UpdateProduct(product.ProductId, model))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View("UnauthorizedView");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var product = productService.GetProductById(id);
                if (product == null)
                {
                    return View("NotFound");
                }
                
                if (productService.DeleteProduct(product))
                {
                    return RedirectToAction("Index");
                }
                return View();
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
        public IActionResult Create (ProductViewModel model)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                productService.CreateProduct(model);
                return RedirectToAction("Index");                                              
            }
            return View("UnauthorizedView");
        }
    }

}
