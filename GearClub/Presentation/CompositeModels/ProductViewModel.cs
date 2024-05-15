using GearClub.Domain.Models;

namespace GearClub.Presentation.CompositeModels
{
    public class ProductViewModel
    {        
        public Product? Product { get; set; }
        public IFormFile? PreviewImage { get; set; }
        public List<IFormFile>? DetailImages { get; set; }       
    }
}
