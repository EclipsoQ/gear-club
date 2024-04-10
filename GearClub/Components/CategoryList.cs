using Microsoft.AspNetCore.Mvc;
using GearClub.Models;
using GearClub.Repositories;
using GearClub.CompositeModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Specialized;

namespace GearClub.Components
{
    public class CategoryList : ViewComponent
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Category_Product> _categoryProductRepository;
        public CategoryList(IRepository<Category> categoryRepository, IRepository<Category_Product> repository)
        {
            _categoryRepository = categoryRepository;
            _categoryProductRepository = repository;
        }
                
        public IViewComponentResult Invoke ()
        {
            var categories = _categoryRepository.GetAll();

            List<CategoryListModel> categoryList = new List<CategoryListModel>();
            foreach (var category in categories)
            {
                int count = _categoryProductRepository.CountById(category.CategoryId);
                categoryList.Add(new CategoryListModel
                {
                    Category = category,
                    ProductsInCategory = count
                });
            }

            return View(categoryList);
        }
    }
}
