using Microsoft.AspNetCore.Mvc;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Presentation.Components
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
            return View(_repository.GetAll().Where(c => !c.IsDeleted));
        }
    }
}
