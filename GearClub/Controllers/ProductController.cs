using GearClub.Repositories;
using Microsoft.AspNetCore.Mvc;
using GearClub.Models;
using X.PagedList;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GearClub.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepo;
        public ProductController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
                
        public IActionResult ViewProducts (int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;
            return View(_productRepo.GetAll().ToPagedList(pageNumber, pageSize));
        }
    }
}
