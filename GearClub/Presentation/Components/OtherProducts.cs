using GearClub.Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GearClub.Presentation.Components
{
    public class OtherProducts : ViewComponent
    {
        private readonly IProductService productService;
        public OtherProducts(IProductService productService)
        {
            this.productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var products = productService.GetAllProducts().OrderBy(p => Guid.NewGuid()).Take(5);
            return View(products);
        }
    }
}
