using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace GearClub.Presentation.Components
{
    public class FeaturedProducts : ViewComponent
    {
        private readonly IRepository<Product> _productRepository;
        public FeaturedProducts(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            var products = _productRepository.GetAll().OrderBy(p => Guid.NewGuid()).Take(8);
            return View(products);
        }
    }
}
