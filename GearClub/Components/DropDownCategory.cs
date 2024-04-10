using GearClub.Repositories;
using Microsoft.AspNetCore.Mvc;
using GearClub.Models;

namespace GearClub.Components
{
    public class DropDownCategory : ViewComponent
    {
        private readonly IRepository<Category> _repository;
        public DropDownCategory(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_repository.GetAll());
        }
    }
}
