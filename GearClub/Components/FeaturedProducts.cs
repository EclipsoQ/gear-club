using GearClub.Models;
using GearClub.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GearClub.Components
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
            var products = _productRepository.GetAll().Take(8);            
            return View(products);
        }
    }
}
