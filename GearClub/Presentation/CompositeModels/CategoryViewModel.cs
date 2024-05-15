using GearClub.Domain.Models;

namespace GearClub.Presentation.CompositeModels
{
    public class CategoryViewModel
    {
        public Category? Category { get; set; }
        public IFormFile? PreviewImage { get; set; }
    }
}
